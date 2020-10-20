﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevIO.App.Data;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces;
using AutoMapper;
using DevIO.Business.Models;

namespace DevIO.App.Controllers
{
    public class ProdutosController : Controller
    {
        // private readonly ApplicationDbContext _context;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository produtoRepository, IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedores()));
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null)
            {
                return NotFound();
            }

            return View(produtoViewModel);
        }

        // GET: Produtos/Create
        public async Task<IActionResult> Create()
        {
            //ViewData["FornecedorId"] = new SelectList(_context.Set<FornecedorViewModel>(), "Id", "Documento");
            ProdutoViewModel produtoViewModel = await PopularFornecedores(new ProdutoViewModel());
            return View(produtoViewModel);
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await PopularFornecedores(produtoViewModel);

            if (!ModelState.IsValid) return View(produtoViewModel);

            await _produtoRepository.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            return View(produtoViewModel);

            //produtoViewModel.Id = Guid.NewGuid();
            //    _context.Add(produtoViewModel);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //ViewData["FornecedorId"] = new SelectList(_context.Set<FornecedorViewModel>(), "Id", "Documento", produtoViewModel.FornecedorId);
            
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);
            if (produtoViewModel == null)
            {
                return NotFound();
            }

            // var produtoViewModel = await _context.ProdutoViewModel.FindAsync(id);
            //ViewData["FornecedorId"] = new SelectList(_context.Set<FornecedorViewModel>(), "Id", "Documento", produtoViewModel.FornecedorId);
            return View(produtoViewModel);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(produtoViewModel);

            await _produtoRepository.Atualizar(_mapper.Map<Produto>(produtoViewModel));

            return RedirectToAction("Index");

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(produtoViewModel);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ProdutoViewModelExists(produtoViewModel.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["FornecedorId"] = new SelectList(_context.Set<FornecedorViewModel>(), "Id", "Documento", produtoViewModel.FornecedorId);
            //return View(produtoViewModel);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await ObterProduto(id);
            if(produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await ObterProduto(id);
            if (produto == null)
            {
                return NotFound();
            }

            await _produtoRepository.Remover(id);

            return RedirectToAction("Index");

            //var produtoViewModel = await _context.ProdutoViewModel.FindAsync(id);
            //_context.ProdutoViewModel.Remove(produtoViewModel);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutosPorFornecedor(id));
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }

        private async Task<ProdutoViewModel> PopularFornecedores(ProdutoViewModel produto)
        {
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }

    }
}
