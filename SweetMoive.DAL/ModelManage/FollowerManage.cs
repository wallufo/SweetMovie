using SweetMoive.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL.ModelManage
{
    public class FollowerManage:BaseMannage<Follower>
    {
        public Follower Find(int userID,int followerID)
        {
            return base.Repository.Find(p => p.UserID == userID && p.Followed_user == followerID);
        }
    }
}
