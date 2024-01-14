using System.Text.Json.Serialization;

namespace Services.Doctors.Core.DataAccess.DTOs.Elasticsearch;

public class ElasticsearchSearchResponse<TSourceEntity>
{
    public ElasticsearchSearchResponseHits<TSourceEntity> hits { get; set; }
}

public class ElasticsearchSearchResponseHits<TSourceEntity>
{
    public ElasticsearchSearchResponseHitsTotal total { get; set; }
    public List<ElasticsearchSearchResponseHitsItem<TSourceEntity>> hits { get; set; }
}

public class ElasticsearchSearchResponseHitsTotal
{
    public int value { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ElasticsearchSearchResponseHitsTotalRelation relation { get; set; }
}

public class ElasticsearchSearchResponseHitsItem<TSourceEntity>
{
    public string _index { get; set; }
    public string _id { get; set; }
    public float _score { get; set; }
    public TSourceEntity _source { get; set; }
}

public enum ElasticsearchSearchResponseHitsTotalRelation
{
    eq,
    gte
}