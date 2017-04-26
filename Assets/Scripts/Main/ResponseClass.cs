using System;
using System.Collections.Generic;

[System.Serializable]
public class Meta {
	public string offset;
	public string limit;
	public int count;
	public int program;
	public int clip;
	public string q;
}

[System.Serializable]
public class Description {
	public string fi;
}

[System.Serializable]
public class Video {
}

[System.Serializable]
public class Title {
	public string fi;
	public string sv;
	public string en;
}

[System.Serializable]
public class CoverImage {
	public string id;
	public bool available;
	public string type;
	public int version;
}

[System.Serializable]
public class Interaction {
	public Title title;
	public string type;
	public string url;
}

[System.Serializable]
public class Image {
	public string id;
	public bool available;
	public string type;
	public int version;
}

[System.Serializable]
public class Broader {
	public string id;
}

[System.Serializable]
public class Subject {
	public string id;
	public Title title;
	public string inScheme;
	public string type;
	public string key;
	public Broader broader;
}

[System.Serializable]
public class AvailabilityDescription {
	public string fi;
}

[System.Serializable]
public class PartOfSeries {
	public Description description;
	public object[] creator;
	public string[] alternativeId;
	public string type;
	public Title title;
	public CoverImage coverImage;
	public object[] countryOfOrigin;
	public Interaction[] interactions;
	public string id;
	public Image image;
	public Subject[] subject;
	public AvailabilityDescription availabilityDescription;
}

[System.Serializable]
public class ContentRating {
	public Title title;
	public object[] reason;
}

[System.Serializable]
public class ItemTitle {
	public string fi;
}

[System.Serializable]
public class Format {
	public string inScheme;
	public string type;
	public string key;
}

[System.Serializable]
public class Audio {
	public string[] language;
	public Format[] format;
	public string type;
}

[System.Serializable]
public class OriginalTitle {
}

[System.Serializable]
public class Service {
	public string id;
}

[System.Serializable]
public class ContentProtection {
	public string id;
	public string type;
}

[System.Serializable]
public class Media {
	public string id;
	public string duration;
	public ContentProtection[] contentProtection;
	public bool? available;
	public string type;
	public bool? downloadable;
}

[System.Serializable]
public class Publisher {
	public string id;
}

[System.Serializable]
public class PublicationEvent {
	public Service service;
	public string startTime;
	public string temporalStatus;
	public string endTime;
	public string type;
	public string duration;
	public string region;
	public string id;
	public Media media;
	public Publisher[] publisher;
}

[System.Serializable]
public class Notation {
	public string value;
	public string valueType;
}

[System.Serializable]
public class Subject2 {
	public string id;
	public Title title;
	public string inScheme;
	public string type;
	public string key;
	public Notation[] notation;
	public Broader broader;
}

[System.Serializable]
public class Datum {
	public Description description;
	public Video video;
	public string typeMedia;
	public object[] creator;
	public PartOfSeries partOfSeries;
	public string indexDataModified;
	public string[] alternativeId;
	public string type;
	public string duration;
	public ContentRating contentRating;
	public Title title;
	public ItemTitle itemTitle;
	public object[] countryOfOrigin;
	public object[] interactions;
	public string id;
	public string typeCreative;
	public Image image;
	public Audio[] audio;
	public OriginalTitle originalTitle;
	public PublicationEvent[] publicationEvent;
	public string collection;
	public Subject2[] subject;
	public object[] subtitling;
}

[System.Serializable]
public class ResponseClass {
	public string apiVersion;
	public Meta meta;
	public Datum[] data;
}