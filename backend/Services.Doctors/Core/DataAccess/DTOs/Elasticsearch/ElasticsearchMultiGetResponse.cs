namespace Services.Doctors.Core.DataAccess.DTOs.Elasticsearch;

public class ElasticsearchMultiGetResponse<TSourceEntity>
{
    public List<ElasticsearchMultiGetResponseDocument<TSourceEntity>> docs { get; set; }
}

public class ElasticsearchMultiGetResponseDocument<TSourceEntity>
{
    public string _index { get; set; }
    public string _id { get; set; }
    public bool found { get; set; }
    public TSourceEntity? _source { get; set; }
}