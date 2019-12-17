using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Models
{
    public class Confeccao
    {
        public Confeccao()
        {

        }
        public Confeccao(TipoProduto tipoProduto)
        {
            this.tipoProduto = tipoProduto;
            this.data = DateTime.Now;
        }
        
        public int confeccaoId { get; set; }
        
        public TipoProduto tipoProduto { get; set; }
        public int tipoProdutoId { get; set; }
        public DateTime data { get; set; }
    }
}
