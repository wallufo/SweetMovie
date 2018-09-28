using SweetMoive.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL
{
    /// <summary>
    /// 创建数据库上下文
    /// </summary>
    public class SweetMovieContext:DbContext
    {
        public SweetMovieContext() : base("SweetMovieConnection")
        {
            Database.SetInitializer<SweetMovieContext>(new SweetMovieDbInitializer<SweetMovieContext>());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<MovieComment> MovieComments { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Administrator> Admministrators { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Like> Likes { get; set; }
        private class SweetMovieDbInitializer<T> : CreateDatabaseIfNotExists<SweetMovieContext>
        {
            protected override void Seed(SweetMovieContext context)
            {
                var Administrators = new List<Administrator> {
                new Administrator { Accounts="Adminer",Password= "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=",CreateTime=DateTime.Now }
                };
                Administrators.ForEach(s => context.Admministrators.Add(s));
                var Users = new List<User>
                {
                    new User { Username="guester",Password= "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=",EmailAdress="877546504@qq.com",Role=DAL.Models.User.Roles.评论专家,SweetScore=60,Userstatus=DAL.Models.User.UserStatus.启用}
                };
                Users.ForEach(s => context.Users.Add(s));
                context.SaveChanges();
                base.Seed(context);
            }
        }
    }
   
    
}
