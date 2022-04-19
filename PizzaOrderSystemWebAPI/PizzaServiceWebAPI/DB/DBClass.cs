using PizzaOrderSystemWebAPI.Model;
using PizzaServiceWebAPI.Model;
using System.Collections.Generic;
using static PizzaServiceWebAPI.Model.PizzaDetails;

namespace PizzaServiceWebAPI.DB
{
    public sealed class DBClass
    {
        #region Singleton Instance
        private static DBClass _instance;
        #endregion
        private DBClass()
        { }

        public static DBClass GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DBClass();
                _instance.setAllValues();
            }
            return _instance;
        }

        public List<UserDetailsModel> UserDetailsModelData;
        public List<PizzaIngredientsDetails> PizzaIngredientsListData;
        public List<PizzaDetails> PizzaListData;
        private void setAllValues()
        {
            UserDetailsModelData = new List<UserDetailsModel>()
            {
                new UserDetailsModel()
                {
                    User_Id=1,
                    First_Name="karan",
                    Last_Name="shinde",
                    Email_Id="karanshinde@gmail.com",
                    UserName="karan",
                    Password="karan123",
                    CreateDate=System.DateTime.Now,
                    CreatedBy="karan",
                    IsActive=true,
                },
                new UserDetailsModel()
                {
                    User_Id=2,
                    First_Name="admin",
                    Last_Name="admin",
                    Email_Id="admin@gmail.com",
                    UserName="admin",
                    Password="admin123",
                    CreateDate=System.DateTime.Now,
                    CreatedBy="admin",
                    IsActive=true,
                }
            };

            PizzaIngredientsListData = new List<PizzaIngredientsDetails>()
            {
                new PizzaIngredientsDetails()
                {
                    Id=1,
                    Name="Pizza Crust",
                    Price=0,
                    Ingredients_Type=PizzaIngredientsDetailsType.PizzaCrust,
                    IngredientsList=new List<PizzaIngredientsList>()
                    {
                        new PizzaIngredientsList()
                        {
                            Id=1,
                            Name="small",
                            Price=50,
                        },
                        new PizzaIngredientsList()
                        {
                            Id=2,
                            Name="medium",
                            Price=100,
                        },
                        new PizzaIngredientsList()
                        {
                            Id=3,
                            Name="large",
                            Price=150,
                        }
                    }
                },
                new PizzaIngredientsDetails()
                {
                    Id=2,
                    Name="Sauce",
                    Price=0,
                    Ingredients_Type=PizzaIngredientsDetailsType.Sauce,
                    IngredientsList=new List<PizzaIngredientsList>()
                    {
                        new PizzaIngredientsList()
                        {
                            Id=1,
                            Name="marinara",
                            Price=50,
                        },
                        new PizzaIngredientsList()
                        {
                            Id=2,
                            Name="cheese",
                            Price=100,
                        },
                        new PizzaIngredientsList()
                        {
                            Id=3,
                            Name="ranch",
                            Price=150,
                        }
                    }
                },
                new PizzaIngredientsDetails()
                {
                    Id=3,
                    Name="Cheese",
                    Price=0,
                    Ingredients_Type=PizzaIngredientsDetailsType.Cheese,
                    IngredientsList=new List<PizzaIngredientsList>()
                    {
                        new PizzaIngredientsList()
                        {
                            Id=1,
                            Name="No",
                            Price=0,
                        },
                        new PizzaIngredientsList()
                        {
                            Id=2,
                            Name="Yes",
                            Price=50,
                        },
                        new PizzaIngredientsList()
                        {
                            Id=3,
                            Name="Extra Cheese",
                            Price=100,
                        }
                    }
                },
                new PizzaIngredientsDetails()
                {
                    Id=4,
                    Name="Toppings",
                    Price=0,
                    Ingredients_Type=PizzaIngredientsDetailsType.Toppings,
                    IngredientsList=new List<PizzaIngredientsList>()
                    {
                        new PizzaIngredientsList()
                        {
                            Id=1,
                            Name="Pepperoni",
                            Price=0,
                        },
                        new PizzaIngredientsList()
                        {
                            Id=2,
                            Name="Mushrooms",
                            Price=50,
                        },
                        new PizzaIngredientsList()
                        {
                            Id=3,
                            Name="Onions",
                            Price=100,
                        },
                        new PizzaIngredientsList()
                        {
                            Id=4,
                            Name="Sausage",
                            Price=150,
                        },
                        new PizzaIngredientsList()
                        {
                            Id=5,
                            Name="Spinach",
                            Price=200,
                        },
                        new PizzaIngredientsList()
                        {
                            Id=6,
                            Name="Bacon",
                            Price=250,
                        },
                        new PizzaIngredientsList()
                        {
                            Id=7,
                            Name="Black olives",
                            Price=300,
                        },
                        new PizzaIngredientsList()
                        {
                            Id=8,
                            Name="Green peppers",
                            Price=350,
                        },
                        new PizzaIngredientsList()
                        {
                            Id=9,
                            Name="Pineapple",
                            Price=400,
                        }
                    }
                }
            };

            PizzaListData = new List<PizzaDetails>()
            {
                new PizzaDetails()
                {
                    Id=1,
                    Name = "Pizza1",
                    IngredientsDetailsList=new List<PizzaIngredientsDetails>()
                {
                        selectIngredients(PizzaIngredientsDetailsType.PizzaCrust,getIngredientListByIds(PizzaIngredientsDetailsType.PizzaCrust,new List<int>{1,3})),
                        selectIngredients(PizzaIngredientsDetailsType.Toppings,getIngredientListByIds(PizzaIngredientsDetailsType.Toppings,new List<int>{1,2,3})),
                }
            },
                new PizzaDetails()
                {
                    Id=2,
                    Name = "Pizza2",
                    IngredientsDetailsList= new List<PizzaIngredientsDetails>()
                    {
                            selectIngredients(PizzaIngredientsDetailsType.Sauce,getIngredientListByIds(PizzaIngredientsDetailsType.Cheese,new List<int>{3})),
                            selectIngredients(PizzaIngredientsDetailsType.Cheese,getIngredientListByIds(PizzaIngredientsDetailsType.Sauce,new List<int>{2})),
                    }
                }
            };

        }

        private static PizzaIngredientsDetails selectIngredients(PizzaIngredientsDetailsType Ingredients_Type, List<PizzaIngredientsList> selectedData)
        {
            try
            {
                var data = _instance.PizzaIngredientsListData.Find(y => y.Ingredients_Type == Ingredients_Type);
                data.IngredientsSelectedList = selectedData;
                return data;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private static List<PizzaIngredientsList> getIngredientListByIds(PizzaIngredientsDetailsType Ingredients_Type, List<int> idLists)
        {
            var list = new List<PizzaIngredientsList>();
            try
            {
                var listData = _instance.PizzaIngredientsListData.Find(y => y.Ingredients_Type == Ingredients_Type).IngredientsList;
                idLists.ForEach(x => list.Add(listData.Find(y => y.Id == x)));
            }
            catch (System.Exception)
            {
                throw;
            }
            return list;
        }

    }
}






