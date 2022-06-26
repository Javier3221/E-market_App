using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_market.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, GetViewModel>
        where SaveViewModel : class
        where GetViewModel : class
    {
        Task<SaveViewModel> Add(SaveViewModel vm);
        Task Update(SaveViewModel vm);
        Task Delete(int id);
        Task<SaveViewModel> GetByIdSaveViewModel(int id);
        Task<List<GetViewModel>> GetAllViewModel();
    }
}
