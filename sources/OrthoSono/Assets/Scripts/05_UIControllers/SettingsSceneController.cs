using UnityEngine;
using System.Collections;

public class SettingsSceneController : UIController
{

	//UI - labels
	public UILabel sceneTitle = null;
	public UILabel sceneSubtitle = null;

	void Awake()
	{
		base.DoAwake ();
	}

	void Start()
	{
		base.DoStart();
		sceneTitle.text = "Orthopedic sonography"; // should be localised
		sceneSubtitle.text = "Choose";  // should be localised

		Debug.Log("On do start ");
		UILoadingIndicatorFactory.Hide();
	}

	protected override void DoUpdate() 
	{
		base.DoUpdate();
	}

	void OnBackButton (GameObject sender)
	{
		LoadPreviousScene();
	}

	void LoadPreviousScene()
	{
		PrepareSceneLoad();
		FadeTransition.PopLevel();
	}

}
