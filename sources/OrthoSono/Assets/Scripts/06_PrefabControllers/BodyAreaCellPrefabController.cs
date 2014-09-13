using UnityEngine;
using System.Collections;

public class BodyAreaCellPrefabController : MonoBehaviour {


	public UILabel titleLabel = null;
	public UILabel authorLabel = null;

	protected UIPanel _clipPanel = null;
	protected UIPanel CachedClipPanel
	{
		get
		{
			if (_clipPanel == null)
				_clipPanel = NGUITools.FindInParents<UIPanel>(gameObject);
			return _clipPanel;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
