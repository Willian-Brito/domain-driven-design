namespace NerdStore.Modules.Vendas.Application.UseCases.ObterEventos.ViewModel;

public class EventosViewModel
{
    public Guid Id { get; set; }
    public string Tipo { get; set; }
    public DateTime DataOcorrencia { get; set; }    
    public string Dados { get; set; }
}