namespace Services.Doctors.Core.DataAccess.DTOs.Elasticsearch;

public class ElasticsearchWildcardQuery<TFields>(TFields fields)
{
    public TFields wildcard { get; set; } = fields;
}