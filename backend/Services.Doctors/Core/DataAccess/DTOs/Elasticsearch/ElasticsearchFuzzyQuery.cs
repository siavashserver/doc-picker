namespace Services.Doctors.Core.DataAccess.DTOs.Elasticsearch;

public class ElasticsearchFuzzyQuery<TFields>(TFields fields)
{
    public TFields fuzzy { get; set; } = fields;
}