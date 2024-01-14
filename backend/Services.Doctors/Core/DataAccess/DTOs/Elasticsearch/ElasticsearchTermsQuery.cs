namespace Services.Doctors;

public class ElasticsearchTermsQuery<TFields>(TFields fields)
{
    public TFields terms { get; set; } = fields;
}