using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Core.Entities;
using ProductManagement.Core.Interfaces;

namespace ProductManagement.Web.Controllers {
    public class ProductController : Controller {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper) {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index() {
            var products = await _productRepository.GetAllAsync();
            // Mapping Poducts to ProductDto
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return View(productDtos);
        }

        public IActionResult Create() {
            return View();
        }     

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto) {
            if (ModelState.IsValid) {
                var product = _mapper.Map<Product>(productDto);
                await _productRepository.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }

        public async Task<IActionResult> Edit(int id) {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            var productDto = _mapper.Map<ProductDto>(product);
            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDto productDto) {          

            if (ModelState.IsValid) {
                var product = await _productRepository.GetByIdAsync(productDto.Id);
                if (product == null)
                    return NotFound();

                
                var createdAt = product.CreatedAt;                
                _mapper.Map(productDto, product);                
                product.CreatedAt = createdAt;                
                await _productRepository.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }

            return View(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id) {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            await _productRepository.DeleteAsync(product.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
