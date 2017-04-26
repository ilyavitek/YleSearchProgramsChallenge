using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class StaticManager {

	#region URLs

	public static string BASE_URL = "https://external.api.yle.fi";
	public static string SEARCH_URL = "/v1/programs/items.json?q=";
	public static string SEARCH_LIMIT_URL = "&limit=";

	public static string AUTH_URL = "&app_id=1398104a&app_key=728d27136d8e9ff90888c568fbf9001c";

	#endregion

	#region Counts

	public static int DEFAULT_SEARCH_PROGRAMS_COUNT = 10;
	public static int INCREMENT_SEARCH_PROGRAMS_COUNT = 10;

	//checked by experiment
	public static int MAX_SEARCH_PROGRAMS_COUNT = 100;

	public static int COUNT_OF_FEW_LAST_ITEMS_TO_UPDATE_LIST = 9;

	#endregion

	#region Texts

	public static string MESSAGE_FINNISH_TITLE_IS_EMPTY = "There is no Finnish title for this program";

	#endregion

}
