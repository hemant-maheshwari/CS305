using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseManager.Service;

namespace ExpenseManager.Controller
{
    public class BaseController<T>
    {
        private RestAPICRUDService<T> RestAPICRUDService;
        public BaseController()
        {
            RestAPICRUDService = new RestAPICRUDService<T>();
        }

        public async Task<bool> createModel(T modelObject)
        {
            return await RestAPICRUDService.createModelAsync(modelObject);
        }

        public async Task<List<T>> getAllModels(int searchId)
        {
            return await RestAPICRUDService.getAllModelAsync(searchId);
        }

        public async Task<T> getModel(int searchId)
        {
            return await RestAPICRUDService.getModelAsync(searchId);
        }

        public async Task<bool> deleteModel(int searchId)
        {
            return await RestAPICRUDService.deleteModelAsync(searchId);
        }

        public async Task<bool> updateModel(T modelObject)
        {
            return await RestAPICRUDService.updateModelAsync(modelObject);
        }
    }
}
