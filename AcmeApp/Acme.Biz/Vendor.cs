using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages the vendors from whom we purchase our inventory.
    /// </summary>
    public class Vendor 
    {
        //  public enum IncludeAddress { Yes, No}
        // public enum sendCopy { Yes, No }



        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Sends an email to welcome a new vendor.
        /// </summary>
        /// <returns></returns>
        public string SendWelcomeEmail(string message)
        {
            var emailService = new EmailService();
            var subject = ("Hello " + this.CompanyName).Trim();
            var confirmation = emailService.SendMessage(subject,
                                                        message, 
                                                        this.Email);
            return confirmation;
        }
        // to make xml comment shortly place the cursor in front of the signature type in 3 backslahes press enter
        /// <summary>
        /// Sends a product order to the vendor
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity)
        {
            //if (product == null)
            //    throw new ArgumentNullException(nameof(product));   //guard clauses: verilen degerlerin kısıtlara uygun olup olmadıgını kontrol icin
            //if (quantity <= 0)
            //    throw new ArgumentOutOfRangeException(nameof(quantity));

            //var success = false;

            //var orderText = "Order from Acme, Inc" + System.Environment.NewLine + "Product: " + product.ProductCode + System.Environment.NewLine + "Quantity: " + quantity;

            //var emailService = new EmailService();
            //var confirmation = emailService.SendMessage("New Order", orderText, this.Email);

            //if (confirmation.StartsWith("Message sent:"))
            //{
            //    success = true;
            //}
            //var operationResult = new OperationResult(success, orderText);
            //return operationResult;

            return PlaceOrder(product, quantity, null, null);  //method chaining

        }
        /// <summary>
        /// Sends a product order to the vendor
        /// </summary>
        /// <param name="product">Product to order</param>
        /// <param name="quantity">Quantity of the product to order</param>
        /// <param name="deliverBy">Requested delivery date</param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy)
        {
            //if (product == null)
            //    throw new ArgumentNullException(nameof(product));   //guard clauses: verilen degerlerin kısıtlara uygun olup olmadıgını kontrol icin
            //if (quantity <= 0)
            //    throw new ArgumentOutOfRangeException(nameof(quantity));
            //if (deliverBy <= DateTimeOffset.Now)
            //    throw new ArgumentOutOfRangeException(nameof(deliverBy));

            //var success = false;

            //var orderText = "Order from Acme, Inc" + System.Environment.NewLine + "Product: " + product.ProductCode + System.Environment.NewLine + "Quantity: " + quantity;

            //if (deliverBy.HasValue)
            //{
            //    orderText += System.Environment.NewLine + "Deliver By: " + deliverBy.Value;
            //}

            //var emailService = new EmailService();
            //var confirmation = emailService.SendMessage("New Order", orderText, this.Email);

            //if (confirmation.StartsWith("Message sent:"))
            //{
            //    success = true;
            //}
            //var operationResult = new OperationResult(success, orderText);
            //return operationResult;

            return PlaceOrder(product, quantity, deliverBy, null);    //method chaining
        }
        /// <summary>
        /// Sends a product order to the vendor
        /// </summary>
        /// <param name="product">Product to order</param>
        /// <param name="quantity">Quantity of the product to order</param>
        /// <param name="deliverBy">Requested delivery date</param>
        /// <param name="instructions">Delivery instructions</param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy= null, string instructions="standart delivery")
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));   //guard clauses: verilen degerlerin kısıtlara uygun olup olmadıgını kontrol icin
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));
            if (deliverBy <= DateTimeOffset.Now)
                throw new ArgumentOutOfRangeException(nameof(deliverBy));

            var success = false;

            //var orderText = "Order from Acme, Inc" + System.Environment.NewLine + "Product: " + product.ProductCode + System.Environment.NewLine + "Quantity: " + quantity;

            //if (deliverBy.HasValue)
            //{
            //    orderText += System.Environment.NewLine + "Deliver By: " + deliverBy.Value;
            //}

            //if (!String.IsNullOrWhiteSpace(instructions))
            //{
            //    orderText += System.Environment.NewLine + "Instructions: " + instructions;
            //}
            //{
            //    orderText += System.Environment.NewLine + "Deliver By: " + deliverBy.Value.ToString("d");
            //}


            //Using StringBuilder features instead of orderText Concatenation here
            //Lets use orderTextBuilder instead of orderText than convert it to orderText strting with ToString() method at last

            var orderTextBuilder = new StringBuilder("Order from Acme, Inc" + System.Environment.NewLine + "Product: " + product.ProductCode + System.Environment.NewLine + "Quantity: " + quantity);

            if (deliverBy.HasValue)
            {
                orderTextBuilder.Append(System.Environment.NewLine + "Deliver By: " + deliverBy.Value);
            }

            if (!String.IsNullOrWhiteSpace(instructions))
            {
                orderTextBuilder.Append(System.Environment.NewLine + "Instructions: " + instructions);
            }
            //{
            //    orderTextBuilder.Append(System.Environment.NewLine + "Deliver By: " + deliverBy.Value.ToString());
            //}
            var orderText = orderTextBuilder.ToString(); 


            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New Order", orderText, this.Email);

            if (confirmation.StartsWith("Message sent:"))
            {
                success = true;
            }
            var operationResult = new OperationResult(success, orderText);
            return operationResult;
        }
        public override string ToString()
        {
            string vendorInfo = "Vendor: " + this.CompanyName;
            // string vendorInfo = null; //eger yukardaki kod yerine asagıdaki calıssaydı nul ref exception verirdi.
            //bunu onlemek icin asagıda vendorInfo dan sonra noktadan once>> null conditional operator "?" kulan (only in c#6)
                                        // veya if ile birlikte isNuulorWhiteSpace metodunu kulan
            string result;
            result = vendorInfo.ToLower();
            result = vendorInfo.ToUpper();
            result = vendorInfo.Replace("Vendor", "Supplier");

            var length = vendorInfo.Length;
            var index = vendorInfo.IndexOf(":");
            var begins = vendorInfo.StartsWith("Vendor");


            return vendorInfo;
        }

        public string PrepareDirections()
        {
            var directions = @"Insert \r\n to define a new line"; //verbatim string literals>> string başına @ koyarak özel karakter olarak algılamamasını saglar
            return directions;                                     //ozellikle file folder adresleri icin kullanılır.
        }
    }
}
