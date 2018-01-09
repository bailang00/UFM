using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    //不同平台下StreamingAssets的路径是不同的，这里需要注意一下。
    public static readonly string PathURL =
#if UNITY_ANDROID
 "file:///E:/Project/Demo/Assets/StreamingAssets/";
#elif UNITY_IPHONE
		Application.dataPath + "/Raw/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
	"file://" + Application.dataPath + "/StreamingAssets/";
#else
        string.Empty;
#endif


    void Start()
    {

        StartCoroutine(LoadMainGameObject(PathURL + "Cube.ab"));
	
	}



    private IEnumerator LoadMainGameObject(string path)
    {
        WWW bundle = new WWW(path);

        yield return bundle;

        //Object obj = Instantiate(bundle.assetBundle.mainAsset);
        //GameObject gameObj = obj as GameObject;
        //Test tt = gameObj.GetComponent<Test>();
        //Debug.LogError(tt.obj.name);

        yield return Instantiate(bundle.assetBundle.mainAsset); ;

        bundle.assetBundle.Unload(false);
    }
}
