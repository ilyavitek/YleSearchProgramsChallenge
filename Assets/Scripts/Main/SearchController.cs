using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

class SearchController: MonoBehaviour {

	#region Editor Variables

	[SerializeField]
	Text m_searchText;

	[SerializeField]
	Transform m_resultsPanelTransform;

	[SerializeField]
	GameObject m_resultPrefab;

	[SerializeField]
	Scrollbar m_scrollbar;

	#endregion

	#region Private Variables

	string m_searchRequest;

	List<string> titles;

	ResponseClass rc;

	int m_countProgramsToSearch;

	bool m_isNewSearch = false;
	bool m_canUpdateList = false;

	float m_heightOfOneResult;

	RectTransform m_resultsPanelRectTransform;

	#endregion

	#region Behaviour Overrides

	void Start () {
		m_countProgramsToSearch = StaticManager.DEFAULT_SEARCH_PROGRAMS_COUNT;
		m_heightOfOneResult = Screen.height / 4;
		m_resultsPanelRectTransform = m_resultsPanelTransform.GetComponent<RectTransform> ();
		m_searchRequest = string.Empty;
		titles = new List<string> ();
	}

	void Update () {
		float currentScrolledPosition = m_resultsPanelRectTransform.localPosition.y;
		float sizeOfResultsPanel = m_resultsPanelRectTransform.sizeDelta.y;
		float sizeOfLastFewItems = StaticManager.COUNT_OF_FEW_LAST_ITEMS_TO_UPDATE_LIST * m_heightOfOneResult;

		bool isPartOfContentToUpdateScrolled = currentScrolledPosition > (sizeOfResultsPanel - sizeOfLastFewItems);
		bool isCountOfResultsIsLessThanMax = m_countProgramsToSearch < StaticManager.MAX_SEARCH_PROGRAMS_COUNT;
		bool shouldSendRequestWithIncreasedLimit = m_canUpdateList && isPartOfContentToUpdateScrolled && isCountOfResultsIsLessThanMax && titles.Count > 0;
		                                   
		if (shouldSendRequestWithIncreasedLimit) {
			SendRequestWithIncreasedLimit ();
		}
	}

	#endregion

	#region Public Methods

	public void NewSearch () {
		if (m_searchText.text == string.Empty) {
			CleanResults ();
			return;
		}

		if (!m_searchRequest.Equals (m_searchText.text)) {
			StopAllCoroutines ();
			m_isNewSearch = true;
			m_countProgramsToSearch = StaticManager.DEFAULT_SEARCH_PROGRAMS_COUNT;
			m_searchRequest = m_searchText.text;
			StartCoroutine (Request ());
		}
	}

	#endregion

	#region Private Methods

	void CleanResults () {
		int childCount = m_resultsPanelRectTransform.childCount;
		for (int i = 1; i <= childCount; i++) {
			m_resultsPanelRectTransform.GetChild (0).Recycle ();
		}

		m_resultsPanelRectTransform.localPosition = Vector3.zero;
	}

	void GetResultTitlesToListFromJSON (string json) {
		rc = JsonUtility.FromJson<ResponseClass> (json);

		titles.Clear ();

		for (int i = 0; i < rc.data.Length; i++) {
			titles.Add (rc.data [i].title.fi);
		}
	}

	void ChangeSizeOfResultPanel () {
		m_resultsPanelRectTransform.sizeDelta = new Vector2 (m_resultsPanelRectTransform.sizeDelta.x, titles.Count * m_heightOfOneResult);
	}

	void SpawnResults (int startIndex) {
		for (int i = startIndex; i < titles.Count; i++) {
			GameObject res = m_resultPrefab.Spawn (m_resultsPanelTransform);

			res.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0f, m_heightOfOneResult);

			string resultTextToShow = (titles [i] == null) ? StaticManager.MESSAGE_FINNISH_TITLE_IS_EMPTY : titles [i];
			res.GetComponentInChildren<Text> ().text = resultTextToShow;
		}
	}

	void SendRequestWithIncreasedLimit () {
		m_countProgramsToSearch += StaticManager.INCREMENT_SEARCH_PROGRAMS_COUNT;
		m_canUpdateList = false;
		m_isNewSearch = false;
		StartCoroutine (Request ());
	}

	string GetSearchRequestUri () {
		return StaticManager.BASE_URL + StaticManager.SEARCH_URL
		+ m_searchRequest + StaticManager.SEARCH_LIMIT_URL
		+ m_countProgramsToSearch.ToString () + StaticManager.AUTH_URL;
	}

	IEnumerator Request () {		
		UnityWebRequest www = UnityWebRequest.Get (GetSearchRequestUri ());

		yield return www.Send ();

		if (www.isError) {
			Debug.Log (www.error);
		} else {
			GetResultTitlesToListFromJSON (www.downloadHandler.text);

			if (m_isNewSearch) {
				CleanResults ();
				SpawnResults (0);
			} else {
				SpawnResults (m_countProgramsToSearch - StaticManager.INCREMENT_SEARCH_PROGRAMS_COUNT);
			}

			ChangeSizeOfResultPanel ();

			m_canUpdateList = true;
			m_isNewSearch = false;
		}
	}

	#endregion
}