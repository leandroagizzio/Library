using CoreLibrary.Models.Interfaces;
using CoreLibrary.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Library.Helper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.Controllers
{
    public abstract class BaseCrudController<T> : Controller where T : class, ICrudModel
    {
        private readonly IBaseCrudRepository<T> _baseCrudRepository;
        private readonly string _modelName;

        public BaseCrudController(IBaseCrudRepository<T> baseCrudRepository, string modelName)
        {
            _baseCrudRepository = baseCrudRepository;
            _modelName = modelName;
        }

        public virtual IActionResult Index() {
            var models = _baseCrudRepository.ReadAll();
            return View(models);
        }

        public virtual IActionResult Create() {
            return View();
        }

        public virtual IActionResult Edit(int id) {
            var model = _baseCrudRepository.Read(id);
            return View(model);
        }

        public virtual IActionResult DeleteConfirmation(int id) {
            var model = _baseCrudRepository.Read(id);
            return View(model);
        }

        public virtual IActionResult Delete(int id) {
            try {
                var modelName = _baseCrudRepository.Read(id)?.Name;
                bool isDeleted = _baseCrudRepository.Delete(id);

                if (isDeleted) {
                    TempData[HelperData.GetSuccessTempDataKey] = $"{_modelName} {modelName} deleted with success";
                } else {
                    TempData[HelperData.GetErrorTempDataKey] = "The deletion did not work";
                }
            } catch (Exception error) {
                TempData[HelperData.GetErrorTempDataKey] = $"Error generated: {error.Message}";
            }
            return GoToIndex();
        }

        protected IActionResult GoToIndex() => RedirectToAction("Index");


        [HttpPost]
        public virtual IActionResult Create(T model) {
            return ValidateAndChange(x => _baseCrudRepository.Create(x), model, 
                $"{_modelName} {model.Name} created with success", "Error in creating");
        }

        [HttpPost]
        public virtual IActionResult Edit(T model) {
            return ValidateAndChange(x => _baseCrudRepository.Update(x), model, 
                $"{_modelName} {model.Name} updated with success", "Error in updating", "Edit");
        }

        private IActionResult ValidateAndChange(Func<T,T?> funcChange, T model, string successMessage, string errorMessage) {
            return ValidateAndChange(funcChange, model, successMessage, errorMessage, string.Empty);
        }
        private IActionResult ValidateAndChange(Func<T,T?> funcChange, T model,
            string successMessage, string errorMessage, string viewString) {
            try {
                if (ModelState.IsValid) {
                    var modelDB = funcChange.Invoke(model);
                    if (modelDB is not null)
                        TempData[HelperData.GetSuccessTempDataKey] = successMessage;
                    else
                        TempData[HelperData.GetErrorTempDataKey] = $"Error, {errorMessage}";
                    return GoToIndex();
                }
                if (string.IsNullOrEmpty(viewString)) 
                    return View(model);
                return View(viewString, model);
            } catch (Exception error) {
                TempData[HelperData.GetErrorTempDataKey] = $"Error, {errorMessage}, message: {error.Message}";
                return GoToIndex();
            }
        }


    }
}
