using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configuration
{
    //classe que representa o contexto do banco de dados, herdando do IdentityDbContext com suporte ao ApplicationUser
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }
        //construtor que recebe as opções de configuração e passa para a classe base

        public DbSet<Produto> Produto { get; set; }
        //representa a tabela Produto no banco

        public DbSet<CompraUsuario> CompraUsuario { get; set; }
        //representa a tabela CompraUsuario no banco

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        //representa explicitamente a tabela de usuários (embora o Identity já gerencie isso)

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConectionConfig());
                //define a string de conexão para usar SQL Server

                base.OnConfiguring(optionsBuilder);
                //chama a configuração base do Entity Framework
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
            //configura a tabela do Identity para usar "AspNetUsers" e define a chave primária como o Id

            base.OnModelCreating(builder);
            //chama a configuração padrão do Identity
        }

        private string GetStringConectionConfig()
        {
            string strCon = "Data Source=TIMEWARE052;Initial Catalog=DDD_ECOMMERCE;User ID=sa;Password=123456;Integrated Security=False;";
            //string de conexão usada para conectar ao SQL Server

            return strCon;
            //retorna a string de conexão
        }
    }
}
