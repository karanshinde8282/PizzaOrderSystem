using System;
using System.Collections.Generic;
using PizzaOrderSystemWebAPI.Common;
using PizzaServiceWebAPI.DB;
using PizzaServiceWebAPI.Model;

namespace PizzaOrderSystemWebAPI.DataModel
{
    public class ApiDataModel
    {
        #region Singleton Instance
        public DBClass _DBInstance;
        private static ApiDataModel _instance;
        public static ApiDataModel Instance => _instance ?? (_instance = new ApiDataModel());
        #endregion

        public ApiDataModel()
        {
            _DBInstance = DBClass.GetInstance();
        }

        public List<PizzaDetails> GetPizzaListBy(int Id)
        {
            List<PizzaDetails> pizzaDetailList = new List<PizzaDetails>();
            try
            {
                if (Id > 0)
                {
                    if (_DBInstance.PizzaListData.FindIndex(x => x.Id == Id) > 0)
                    {
                        pizzaDetailList.Add(_DBInstance.PizzaListData.Find(x => x.Id == Id));
                    }
                }
                else
                {
                    pizzaDetailList = _DBInstance.PizzaListData;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(LogType.Exception, ex);
                throw ex;
            }
            return pizzaDetailList;
        }
        public List<PizzaIngredientsDetails> GetPizzaIngredientsListData()
        {
            return _DBInstance.PizzaIngredientsListData;
        }
    }
}
