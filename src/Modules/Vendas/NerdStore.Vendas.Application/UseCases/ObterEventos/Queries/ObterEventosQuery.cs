using MediatR;
using NerdStore.Modules.Core.Data.EventSourcing;
using NerdStore.Modules.Vendas.Application.UseCases.ObterEventos.ViewModel;

namespace NerdStore.Modules.Vendas.Application.UseCases.ObterEventos.Queries;

public class ObterEventosQuery : IRequest<IEnumerable<EventosViewModel>>
{
    public Guid AggregateId { get; set; }

    public class ObterEventosQueryHandler : IRequestHandler<ObterEventosQuery, IEnumerable<EventosViewModel>>
    {
        private readonly IEventSourcingRepository _eventSourcingRepository;
        
        public ObterEventosQueryHandler(IEventSourcingRepository eventSourcingRepository)
        {
            _eventSourcingRepository = eventSourcingRepository;
        } 

        public async Task<IEnumerable<EventosViewModel>> Handle(ObterEventosQuery request, CancellationToken cancellationToken)
        {
            var eventos = await _eventSourcingRepository.ObterEventos(request.AggregateId);

            if (!eventos.Any()) return null;

            var viewModel = new List<EventosViewModel>();

            foreach (var evento in eventos)
            {
                viewModel.Add(new EventosViewModel
                {
                    Id = evento.Id,
                    Tipo = evento.Tipo,
                    DataOcorrencia = evento.DataOcorrencia,
                    Dados = evento.Dados,
                });
            }

            return viewModel;
        }
    }
}