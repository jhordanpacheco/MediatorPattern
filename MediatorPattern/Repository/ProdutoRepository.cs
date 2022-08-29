using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatorPattern.Domain.Entity;

namespace MediatorPattern.Repository
{
    public class PessoaRepository : IRepository<Pessoa>
    {
        private static Dictionary<int, Pessoa> pessoas = new Dictionary<int, Pessoa>();

        public async Task<IEnumerable<Pessoa>> GetAll()
        {
            return await Task.Run(() => pessoas.Values.ToList());
        }

        public async Task<Pessoa> Get(int id)
        {
            return await Task.Run(() => pessoas.GetValueOrDefault(id));
        }

        public async Task Add(Pessoa produto)
        {
            await Task.Run(() => pessoas.Add(produto.Id, produto));
        }

        public async Task Edit(Pessoa produto)
        {
            await Task.Run(() =>
            {
                pessoas.Remove(produto.Id);
                pessoas.Add(produto.Id, produto);
            });
        }

        public async Task Delete(int id)
        {
            await Task.Run(() => pessoas.Remove(id));
        }

        public Dictionary<int, Pessoa> GetProdutos()
        {
            pessoas.Add(1, new Pessoa { Id = 1, Nome = "Jhordan", Idade = 25 });
            pessoas.Add(2, new Pessoa { Id = 2, Nome = "Marcelo Grohe", Idade = 35 });
            pessoas.Add(3, new Pessoa { Id = 3, Nome = "Post Malone", Idade = 27 });

            return pessoas;
        }

        public Int64 MaiorId()
        {
            if (pessoas != null && pessoas.Count > 0)
            {
                Int64 maiorId = Convert.ToInt64(pessoas.OrderByDescending(x => x.Key).First().Key);

                return maiorId;
            }
            else
                return 0;
        }

        public PessoaRepository()
        {
            pessoas = GetProdutos();
        }
    }
}