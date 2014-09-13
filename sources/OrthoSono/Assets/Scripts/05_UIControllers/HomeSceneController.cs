using UnityEngine;
using System.Collections;

public class HomeSceneController : UIController
{
	//Managers
	private DataManager _dataManager;

	//UI - labels
	public UILabel sceneTitle = null;
	public UILabel sceneSubtitle = null;

	//scroll view, cell prefab
	public UIScrollView scrollView = null;
	public  UIGridExt grid = null;
	public	GameObject areaCellPrefab = null;
	public Vector4 scrollViewInset = Vector4.zero;

	void Awake()
	{
		DoAwake ();
	}

	protected override void DoStart()
	{
		base.DoStart();
		_dataManager = DataManager.Instance;

		if (Application.isPlaying) 
		{
			//premiumBoostLabel.text = GameStrings.GetLocalizedString (GameStrings.RecipesCraft_PremiumBoosts_Label);
			//ingredientsExistedLabel.text = GameStrings.GetLocalizedString (GameStrings.RecipesCraft_IngredientsExisted_Label);
			Reposition ();
			sceneTitle.text = "Orthopedic sonography"; // should be localised
			sceneSubtitle.text = "Choose";  // should be localised
			SetupUI();
		}
	}

	protected override void DoUpdate() 
	{
		base.DoUpdate();
		
		if (!Application.isPlaying)
		{
			Reposition();
			UILoadingIndicatorFactory.Hide();
		}
	}

	void OnBackButton (GameObject sender)
	{
		Debug.Log("On Back Button");


	}

	void OnSignInOrSynchronize(GameObject sender)
	{
		Debug.Log("OnSignInOrSynchronize");
	}

	void Reposition()
	{
		// возможная пересчет размеров таблицы под универсальный размер экрана
		/*
		scrollView.pivot = UIWidget.Pivot.TopLeft;
		float xPos = -camWidth/2f + scrollViewInset.x;
		float yPos = camHeight/2f - scrollViewInset.y;
		float w = camWidth - scrollViewInset.x - scrollViewInset.z;
		float h = camHeight - scrollViewInset.y - scrollViewInset.w; 
		scrollView.transform.localPosition = new Vector3(xPos, yPos, scrollView.transform.localPosition.z);
		scrollView.Size = new Vector2(w, h);
		*/
	}

	void SetupTableGrid()
	{	

		grid.RemoveAllCells();

		// префаб - то есть "заготовку" строчки можно проинициализировать и программным способом
		//GameObject gridPrefab_ = Resources.Load(GameConstants.RecipesCraftIngredientIconPrefabsPath) as GameObject;
		// но в нашем случае переменная grid проиинициилизирована через редактор юнити

		GameObject gridGameObject = grid.gameObject;
		ArrayList responseResult =  DataManager.ReadDB(DataManager.GET_SCENE_DTO);
		foreach (SceneDTO scene in responseResult)
		{
			GameObject go =  NGUITools.AddChild(gridGameObject,areaCellPrefab) as GameObject;
			BodyAreaCellPrefabController controller = go.GetComponent<BodyAreaCellPrefabController>();
			if(controller!= null)
			{
				controller.titleLabel.text = scene.sceneName;
				controller.authorLabel.text = "Author: Statsenko E.A., Aбрамчик РР";// scene.sceneTitle;
			}
		}
		grid.Reposition();

	}

	// Button Messages

	void OnSettingsButton ()
	{
		PrepareSceneLoad();
		Debug.Log("OnSettingsButton");
		//Application.LoadLevel("SettingsScene");
		FadeTransition.PushLevel("SettingsScene");
	}

	

		
	void LoadPreviousScene()
	{
		PrepareSceneLoad();
		FadeTransition.PopLevel();
	}

	void SetupUI()
	{
		StopCoroutine("SetupUICoroutine");
		Vector2 dragAmount = scrollView.DragAmount;
		
		//remove current table cells
		grid.hideInactive = false;
		grid.RemoveAllCells();
		//recipesCells.Clear();
		StartCoroutine("SetupUICoroutine", dragAmount);
	}
	
	IEnumerator SetupUICoroutine(Vector2 dragAmount)
	{
		// это заготовка для динамической
		// сборки текстурного атласа 
		/*
		Dictionary<string, Texture2D> texturesDict = new Dictionary<string, Texture2D>();
		string[] commonImages = new string[]
		{
			GameConstants.ready_icon_imgName, 
			GameConstants.defMoney_icon_2_imgName, 
			GameConstants.premMoney_icon_2_imgName, 
			GameConstants.exp_icon_2_imgName,
			GameConstants.count_bg_1_imgName,
			
			GameConstants.ingredients_back_texture, 
			GameConstants.ingredients_back_label
		} ;
		
		int rsPerFrame = 10;
		int rsLoadedInFrame = 0;
		foreach (string imageName in commonImages)
		{
			AddImageWithPathNameAndPathToDictionaryIfNeeded(imageName, GameConstants.CommonImagesPath + imageName, texturesDict);
			rsLoadedInFrame++;
			if (rsLoadedInFrame >= rsPerFrame )
			{
				rsLoadedInFrame = 0;
				yield return null;
			}
		}
		
		foreach( int ingredientId in User.GetIngredientsManager.SortedUnpremiumIngredientsList())
		{
			Ingredient ingredient = User.GetIngredientsManager.GetIngredient(ingredientId);
			AddImageWithPathNameAndPathToDictionaryIfNeeded(ingredient.IconPath, ingredient.IconPath, texturesDict);
			rsLoadedInFrame++;
			if (rsLoadedInFrame >= rsPerFrame )
			{
				rsLoadedInFrame = 0;
				yield return null;
			}
		}
		
		foreach (string iconPath in User.GetRecipesManager.AllPathForRecipes) 
		{
			AddImageWithPathNameAndPathToDictionaryIfNeeded(iconPath,iconPath,texturesDict);
			rsLoadedInFrame++;
			if (rsLoadedInFrame >= rsPerFrame )
			{
				rsLoadedInFrame = 0;
				yield return null;
			}
		}
		
		_atlas = new Texture2DAtlas(texturesDict, Texture2DAtlas.defaultScale);
		yield return null;
		*/

		UILoadingIndicatorFactory.Hide();
		SetupTableGrid();

		yield return null;
	}
	
	
	//Scenes management
	void PrepareSceneLoad()
	{
		StopCoroutine("SetupUICoroutine");
		
		//RemoveAllEventListners(); // if they were set 
		UIAlertViewFactory.Destroy();
	}
	
}
	


