using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using Api.Models;
using Asp.Versioning;

namespace MvcWithApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ShoesController : ControllerBase
    {
        private readonly IShoeRetrievalService shoeRetrievalService;
        public ShoesController(IShoeRetrievalService shoeRetrievalService)
        {
            this.shoeRetrievalService = shoeRetrievalService;   
        }
        //https://code-maze.com/dynamic-queries-expression-trees-csharp/
        //https://code-maze.com/paging-aspnet-core-webapi/
        //https://code-maze.com/sorting-aspnet-core-webapi/

        [HttpPost]
        [Route("GetSortedFilteredPagedResponse")]
        public IActionResult GetSortedFilteredPagedResponse([FromBody]
            ShoesParameters shoesParameters)
        {
            //https://stackoverflow.com/questions/58150652/asp-net-core-3-0-frombody-string-content-returns-the-json-value-could-not-be
            //因.net core原生json物件無法處理dictionary組合,
            //(物件取出型態會變成System.Text.Json.JsonElement,要一個一個轉string),
            //改用AddNewtonsoftJson
            var shoes = shoeRetrievalService.GetShoes();
            var filtered = shoes;
            if (shoesParameters.Filters.Count>0)
            { 
                var expression = shoes.CreateMultiEqualExpression(shoesParameters.Filters);
                var predicate = ExpressionCacheHelper.GetPredicate(expression);
                filtered = shoes.Where(predicate).AsQueryable();
            }
            var pagedShoes = shoeRetrievalService.GetPagedSortingShoes(
                shoeParameters: shoesParameters,
                shoes: filtered);

            ApiResponse apiResponse = new ApiResponse(pagedShoes, pageNumber: pagedShoes.CurrentPage,
                pageSize: pagedShoes.PageSize,
                totalPages: pagedShoes.TotalPages,
                totalRecords: pagedShoes.TotalCount);

            return Ok(apiResponse);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()

        {
            return Ok(shoeRetrievalService.GetShoes());
        }
        [HttpGet]
        [Route("GetByName")]
        public IActionResult GetByName()

        {
            var shoes = shoeRetrievalService.GetShoes();
            IDictionary<string,object> keyValuePairs = 
                new Dictionary<string,object>();
            keyValuePairs.Add("Name", "Pegasus Trail 3");
            var expression = shoes.CreateMultiEqualExpression(keyValuePairs);
            var predicate = ExpressionCacheHelper.GetPredicate(expression);
            var result = shoes.Where(predicate).ToList();
            return Ok(result);
        }
        [HttpGet]
        [Route("GetByRetailName")]
        public IActionResult GetByRetailName()

        {
            var shoes = shoeRetrievalService.GetShoes();
            var expression = shoes.CreateNestedExpression("Retailer.Name","A");
            var predicate = ExpressionCacheHelper.GetPredicate(expression);
            var result = shoes.Where(predicate).ToList();
            return Ok(result);
        }
        [HttpGet]
        [Route("GetByPriceBetween")]
        public IActionResult GetByPriceBetween()

        {
            var shoes = shoeRetrievalService.GetShoes();
            var expression = shoes.CreateBetweenExpression("Price", 119M, 119M);
            var predicate = ExpressionCacheHelper.GetPredicate(expression);
            var result = shoes.Where(predicate).ToList();
            return Ok(result);
        }
        [HttpGet]
        [Route("GetByLikeBrand")]
        public IActionResult GetByLikeBrand()

        {
            var shoes = shoeRetrievalService.GetShoes();
            var expression = shoes.CreateContainsExpression("Brand", "ik");
            var predicate = ExpressionCacheHelper.GetPredicate(expression);
            var result = shoes.Where(predicate).ToList();
            return Ok(result);
        }
        [HttpGet]
        [Route("GetBySortedPriceAndSecondPaged")]
        public IActionResult GetBySortedPriceAndSecondPaged()

        {
            ShoesParameters shoesParameters = new ShoesParameters();
            shoesParameters.PageNumber = 1;
            shoesParameters.PageSize = 6;
            shoesParameters.OrderBy = "price desc";
            var shoes = shoeRetrievalService.GetShoes();
            return Ok(shoeRetrievalService.GetPagedSortingShoes(
                shoeParameters: shoesParameters,
                shoes: shoes));
        }

    }
}
