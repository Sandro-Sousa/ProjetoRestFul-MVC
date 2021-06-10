using DataAcess;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class ProdutoService
    {
        private readonly Context context;

        public ProdutoService(Context _context)
        {
            context = _context;
        }

        public async Task<Produto> BuscarId(int? id)
        {
            return await context.Produtos.FirstOrDefaultAsync(c => c.CodigoId == id);
        }

        public async Task<IEnumerable<Produto>> BuscarProdutos()
        {
            return await context.Produtos.ToListAsync();
        }

        public async Task<Produto> CriarProduto(Produto data)
        {
            context.Produtos.Add(data);
            await context.SaveChangesAsync();
            return data;
        }

        public async Task<Produto> AlterarProduto(Produto data)
        {
            context.Entry(data).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return data;
        }

        public async Task<Produto> DeletarProduto(int? id)
        {
            var data = await context.Produtos.FindAsync(id);
            context.Remove(data);
            await context.SaveChangesAsync();
            return data;
        }
    }
}
