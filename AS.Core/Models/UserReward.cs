using System;

namespace AS.Core.Models
{
    public class UserReward
    {
        public Guid UserId { get; set; }
        public Guid RewardId { get; set; }
        public virtual User User { get; set; }
        public virtual Reward Reward { get; set; }
    }
}