

using FloorMasteryModels;
using FloorMasteryModels.Interfaces;
using FloorMasteryModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMastery.BLL
{
    public class OrderManager
    {
        private IOrderRepo _orderRepo;
        private ITaxRepo _taxRepo;
        private IProductRepo _productRepo;

        public OrderManager(IOrderRepo orderRepo, IProductRepo productRepo, ITaxRepo taxRepo)
        {
            _orderRepo = orderRepo;
            _taxRepo = taxRepo;
            _productRepo = productRepo;
        }

        public DisplayOrderResponse DisplayOrder(DateTime orderDate)
        {
            DisplayOrderResponse response = new DisplayOrderResponse();
            response.ListOfOrders = _orderRepo.LoadListOrder(orderDate);

            if (response.ListOfOrders.Count == 0)
            {
                response.Success = false;
                response.Message = $"There were no orders for {orderDate.ToString("MM/dd/yyyyy")}";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public List<Product> GetListOfProducts()
        {
            return _productRepo.LoadListProducts();
        }

        public EditOrderResponse EditOrder(DateTime orderDate, int orderNumber)
        {
            EditOrderResponse response = new EditOrderResponse();
            response.Order = _orderRepo.LoadOrder(orderDate, orderNumber);
            
            if(response.Order == null)
            {
                response.Success = false;
                response.Message = "Invalid order number entry";
            }
            else
            {
                response.Success = true;
            }

            return response;
        }

        public void RemoveOrder(Order order)
        {
            _orderRepo.PersistOrderRemoval(order);
        }

        public void UpdateOrder(Order order)
        {
           _orderRepo.PersistUpdatedOrder(order);
           
        }

        public void AddToOrderRepo(Order order)
        {
            _orderRepo.SaveOrder(order);
        }

        public AddOrderResponse AddOrder(DateTime dateTime, string customerName, string state, string productType, decimal area, int orderNumber)
        {
            AddOrderResponse response = new AddOrderResponse();

            response.StateTax = _taxRepo.LoadTax(state);
            response.ProductType = _productRepo.LoadProduct(productType);

            if (response.StateTax == null)
            {
                response.Success = false;
                response.Message = $"{state} is not a valid state or not in our network.";
            }
            else if (response.ProductType == null)
            {
                response.Success = false;
                response.Message = $"{productType} is not a vlid product type.";
            }
            else
            {
                response.Success = true;
            }

            if (response.Success)
            {
                response.Order = new Order();

                response.Order.OrderDate = dateTime;
                response.Order.OrderNumber = orderNumber;
                response.Order.CustomerName = customerName;
                response.Order.State = state;
                response.Order.TaxRate = response.StateTax.TaxRate;
                response.Order.ProductType = productType;
                response.Order.CostPerSquareFoot = response.ProductType.CostPerSquareFoot;
                response.Order.LaborCostPerSquareFoot = response.ProductType.LaborCostPerSquareFoot;
                response.Order.Area = area;
            }
            return response;
        }
    }
}
