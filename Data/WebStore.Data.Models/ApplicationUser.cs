// ReSharper disable VirtualMemberCallInConstructor
using System;
using System.Collections.Generic;

using WebStore.Data.Common.Models;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebStore.Data.Models.Enums;

namespace WebStore.Data.Models
{
    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Addresses = new HashSet<Address>();
            this.FavoriteProducts = new HashSet<FavoriteProduct>();
            this.ShoppingCartItems = new HashSet<ShoppingCartItem>();
            this.Orders = new HashSet<Order>();
        }

        public Gender Gender { get; set; }

        [StringLength(maximumLength: 100)]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 100)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<FavoriteProduct> FavoriteProducts { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
