namespace backend.ViewModels;
public class ResultViewModel<TKey>
{
    public TKey? Data { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    public ResultViewModel(TKey data)
        => Data = data;

    public ResultViewModel(List<string> errors)
        => Errors = errors;

    public ResultViewModel(string error)
        => Errors.Add(error);

    public ResultViewModel(TKey data, List<string> error)
    {
        Data = data;
        Errors = error;
    }
}