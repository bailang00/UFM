using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 任务(协程)管理器, 用作AI逻辑实现
///     . 时间单位无关, 可以用秒/毫秒等
///     . 调用 Call/Start 时, 协程会被首先执行一次. 同时, 加入判断检测死循环
///     . 增加任务退出时的析构操作
/// </summary>
public class Task
{
    public static Func<Exception, bool> ExceptionHandler;

    static int _time = 0;                   // 当前时刻
    static Task _cur_t;                     // 当前 Task
    static TaskUnit _cur_u;                 // 当前 TaskUnit
    static ArrayListT<Task> _global_run_list = new ArrayListT<Task>();    // 自动运行的任务列表

    // debug
    static int _calls;                      // 连续 call 次数, 避免死循环
    static Task _last_t;                    // 记录上次执行的任务
    static TaskUnit _last_u;

    // 全局更新
    public static void UpdateAll(int time)
    {
        if (time < _time) return;   // 必须 >=0 

        _time = time;
        _cur_t = null;
        _cur_u = null;
        _calls = 0;
        _last_t = null;
        _last_u = null;
        foreach (var t in _global_run_list.Array) t.Update();
    }

    // 任务单元, 代表一个协程函数
    class TaskUnit
    {
        public IEnumerator e;       // 协程函数
        public object data;         // 外部数据
        public string ret_msg;      // 返回值
        public object ret_val;      // 返回值
        public int time;            // 唤醒时间
        public event Action leave;  // 析构函数
        public List<Task> sub_tasks;
        bool _has_invoke_leave;


        // 调用 leave
        public void InvokeLeave()
        {
            _has_invoke_leave = true;
            if (leave != null)
            {
                try
                {
                    leave();
                }
                catch (Exception e)
                {
                    if (ExceptionHandler == null || !ExceptionHandler(e)) throw;
                }
                finally
                {
                    leave = null;
                }
            }
        }

        // 执行一次更新, 返回是否存活
        public bool Update()
        {
            // 执行
            var old_u = _cur_u;
            _cur_u = _last_u = this;
            var alive = false;
            try
            {
                alive = e.MoveNext();
            }
            catch (Exception e)
            {
                alive = false;
                if (ExceptionHandler == null || !ExceptionHandler(e)) throw;
            }
            finally
            {
                _cur_u = old_u;
            }

            // 结束
            if (!alive)
            {
                InvokeLeave();
            }
            // 暂停
            else
            {
                var wait_time = ConvertToInt(e.Current);
                time = _time + wait_time;
            }

            // done
            return alive && !_has_invoke_leave;
        }

        // 转换为 int
        static int ConvertToInt(object obj)
        {
            var ret = 0;
            if (obj != null)
            {
                if (obj is int)
                {
                    ret = (int)obj;
                }
                else if (!int.TryParse(obj.ToString(), out ret))
                {
                    ret = 0;
                    //Log.LogError("Cant convert to int: " + obj);
                }
                if (ret < 0) ret = 0;
            }
            return ret;
        }
    }

    //
    public string name;     // 名字, 外部识别
    List<TaskUnit> _stack = new List<TaskUnit>();   // 任务队列, [0]最先被执行

    ArrayListT<Task> _run_list;    // 自动执行列表
    bool _is_in_list;

    //
    public Task(bool autoRun)
    {
        //LeakManager.AddWatch(this);

        if (autoRun)
        {
            BindAutoRunList(_global_run_list);
        }
    }
    public Task(ArrayListT<Task> run_list)
    {
        //LeakManager.AddWatch(this);

        if (run_list != null)
        {
            BindAutoRunList(run_list);
        }
    }

    // 绑定到自动运行列表
    void BindAutoRunList(ArrayListT<Task> run_list)
    {
        _run_list = run_list;
        _is_in_list = false;
    }

    // 执行更新
    public void Update()
    {
        // 结束
        if (_stack.Count == 0)
        {
            if (_is_in_list)
            {
                _is_in_list = false;
                _run_list.Remove(this);
            }
            return;
        }

        // 获取第1个执行单元
        var u = _stack[0];
        if (u.time > _time) return;

        // 执行协程, 如果结束, 则删除
        var old_t = _cur_t;
        _cur_t = _last_t = this;
        if (!u.Update())
        {
            // u 执行完毕
            _stack.Remove(u);
        }
        _cur_t = old_t;
    }

    // 停止所有任务, 并调用 leave
    public void Stop()
    {
        if (_stack.Count > 0)
        {
            var arr = _stack.ToArray();
            _stack.Clear();

            // 调用 leave
            foreach (var u in arr)
            {
                u.InvokeLeave();
            }
        }
    }

    // 执行任务
    public void Start(IEnumerator e)
    {
        _Start(e, true);
    }
    void _Start(IEnumerator e, bool bStopAll)
    {
        if (bStopAll) Stop();

        // 执行第一次
        var old_t = _cur_t;
        _cur_t = _last_t = this;
        Call(e);
        _cur_t = old_t;

        // 添加到自动执行队列
        if (_run_list != null && !_is_in_list)
        {
            _is_in_list = true;
            _run_list.Add(this);
        }
    }

    // 中断当前任务, 执行新任务 e
    public void Interrupt(IEnumerator e, CanInterruptHandler handler)
    {
        // 中断之前的任务
        if (handler != null)
        {
            _Interrupt(handler);
        }

        //
        _Start(e, false);
    }
    void _Interrupt(CanInterruptHandler handler)
    {
        var count = _stack.Count;
        if (count == 0) return;

        // 查找 n, 使得 [0, n) 之间的 TaskUnit 可以被停止
        int n = 0;
        for (n = 0; n < count; n++)
        {
            var u = _stack[n];
            var can_interrupt = handler(u.data);
            if (!can_interrupt) break;
        }

        // 删除 [0, n) 之间的 TaskUnit
        if (n > 0)
        {
            TaskUnit[] arr = new TaskUnit[n];
            _stack.CopyTo(0, arr, 0, n);
            _stack.RemoveRange(0, n);
            foreach (var u in arr)
            {
                u.InvokeLeave();
            }
        }
    }
    public delegate bool CanInterruptHandler(object data);      // 返回某个函数释放可被中断

    // 任务状态
    public bool IsDone
    {
        get { return _stack.Count == 0; }
    }
    public bool IsRunning
    {
        get { return _stack.Count > 0; }
    }
    public bool IsCurrent
    {
        get { return Task._cur_t == this; }
    }

    // 返回所有的 data 数组, 堆栈顺序, [0]为最新, [n-1]为最老
    public List<object> GetTmpDatas()
    {
        if (_data_list == null) _data_list = new List<object>();
        _data_list.Clear();
        for (int i = 0; i < _stack.Count; i++)
        {
            var data = _stack[i].data;
            _data_list.Add(data);
        }
        return _data_list;
    }
    static List<object> _data_list;

    #region 静态方法

    // 运行命名协程
    public static void Run(IEnumerator e, string name)
    {
        throw new NotImplementedException();
    }
    public static bool IsRuning(string name)
    {
        throw new NotImplementedException();
    }

    // 运行一个协程
    public static void Run(IEnumerator e)
    {
        var t = new Task(true);
        t.Start(e);
    }

    // 调用一个指函数 e, 等它结束后再返回自己
    // 注意: 一定要用 yield return Task.Call 方式调用, 否则执行结果不可预测
    public static int Call(IEnumerator e)
    {
        if (++_calls > 1000) throw new Exception("检测到死循环!");

        var stack = _cur_t._stack;

        // 清空返回值
        if (stack.Count > 0)
        {
            var u = stack[0];
            u.ret_msg = null;
            u.ret_val = null;
        }

        // 添加任务
        {
            var u = new TaskUnit() { e = e };
            stack.Insert(0, u);     // 添加到头部
        }

        // 执行一次
        _cur_t.Update();

        --_calls;
        return 0;
    }

    // 结束当前函数, 并跳转到新函数 e. 类似状态机中的状态迁移, 或者函数尾调用
    // 注意: 一定要用 yield return Task.Goto 方式调用, 否则执行结果不可预测
    public static int Goto(IEnumerator e)
    {
        // 结束当前函数
        _cur_u.InvokeLeave();

        // 调用新函数
        return Call(e);
    }

    // 运行一个子协程, 当前必须位于协程中, 并且当前协程结束时, 子协程也会被结束
    public static Task RunSubTask(IEnumerator e)
    {
        var sub_tasks = _cur_u.sub_tasks;
        if (sub_tasks == null) _cur_u.sub_tasks = sub_tasks = new List<Task>();

        var t = new Task(_cur_t._run_list);
        sub_tasks.Add(t);
        SetLeave(t.Stop);
        t.Start(e);
        return t;
    }

    public static bool IsSubTaskRuning()
    {
        var sub_task = _cur_u.sub_tasks;
        if (sub_task != null)
        {
            for (int i = 0; i < sub_task.Count; i++)
            {
                if (sub_task[i].IsRunning) return true;
            }
        }
        return false;
    }

    // 终止当前 task, 因为 yield break 只能终止当前协程, 无法终止整个 task
    public static void StopCurrent()
    {
        _cur_t.Stop();
    }

    // 设置当前函数的析构函数
    public static void SetLeave(Action handler)
    {
        _cur_u.leave += handler;
    }
    public static void InvokeLeave()
    {
        _cur_u.InvokeLeave();
    }

    // 设置当前函数私有数据
    public static void SetData(object data)
    {
        _cur_u.data = data;
    }
    public static object GetData()
    {
        return _cur_u.data;
    }

    // 设备返回信息
    [Obsolete("用 SetRetVal 代替")]
    public static void SetReturnMsg(string msg)
    {
        _cur_t._stack[1].ret_msg = msg;
    }
    public static void SetRetVal(object ret_val)
    {
        _cur_t._stack[1].ret_val = ret_val;
    }
    // 获取 Call 一个新函数后, 该函数的返回信息, 如果连续调用了2次 Call, 则前面的被覆盖
    [Obsolete("用 GetRetVal 代替")]
    public static string GetReturnMsg()
    {
        return _cur_u.ret_msg;
    }
    public static object GetRetVal()
    {
        return _cur_u.ret_val;
    }

    #endregion

    #region 其它

    // 延迟调用, key 用于避免多次调用, 可以为 null
    public static Task DelayCall(string key, int wait_time, Action callback)
    {
        // 获取 task
        Task task = null;
        if (key != null)
        {
            _delay_call_tasks.TryGetValue(key, out task);
            if (task == null)
            {
                task = new Task(true);
                _delay_call_tasks.Add(key, task);
            }
        }
        else
        {
            task = new Task(true);
        }

        // 开始协程
        task.Start(_DelayCall(key, wait_time, callback));

        //
        return task;
    }
    static IEnumerator _DelayCall(string key, int wait_time, Action callback)
    {
        yield return wait_time;
        if (key != null) _delay_call_tasks.Remove(key);
        callback();
    }
    static Dictionary<string, Task> _delay_call_tasks = new Dictionary<string, Task>();

    // 取消延迟调用, 如果已经开始, 则会被终止
    public static void DelayCallCancel(string key)
    {
        Task task = null;
        if (_delay_call_tasks.TryGetValue(key, out task))
        {
            _delay_call_tasks.Remove(key);
            task.Stop();
        }
    }


    //
    public override string ToString()
    {
        var sb = new StringBuilder();

        //
        sb.AppendFormat("Task, isRuning:{0}\n", IsRunning);

        //
        foreach (var tu in _stack)
        {
            sb.AppendFormat("  u:{0}, data:{1}\n", tu.e, tu.data);
        }

        //
        return sb.ToString();
    }

    #endregion
}

