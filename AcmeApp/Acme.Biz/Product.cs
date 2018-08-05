using Acme.Common;
using static Acme.Common.LoggingService; //LoggingService sınıfının memberlarını sınıf adını specify etmeden çağırmamıza izin verir
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz  // use <company>.<technology>.<feature> syntax for a good namespace naming with PascalCasing
{
    /// <summary>
    /// Manages product carried in inventory
    /// </summary>
    public class Product
    {
        public const double InchesPerMeter = 39.37; //defining constant dikkat et değiken PascalCase ve direkt sınıftan çağırılır. Degerin hic degısmeyecegını gor

        public readonly decimal MinimumPrice; //we can define readonly field in a class like here..Watch out we used PascalCasing

        #region Constructors

        //constructor chaining
        //default constructor



        public Product()
        {
            Console.WriteLine("Product instance created");
            // this.ProductVendor = new Vendor(); //bunu yapmazsan testte su hatayı verir: Nesne başvurusu bir nesnenin örneğine ayarlanmadı.
            // "Her zaman" ihtiyac icin ust satır uygun; fakat daha etkili olan lazy loading yontemi icin ust satır commentlendi. 
            //Lazy loadingde ProductVendor property nin getter i cagırılırsa vendor instance a ihtiyac olacak. O halde ProductVendor get fonksiyonunu duzenleyelim. 

            this.MinimumPrice = .96m; //we set a default value for readonly field in a constructor
        }
        //parameterized constructor
        public Product(int productId, string productName, string description) : this() //we use "this()" to invoke another constructor
        {
            this.Description = description;
            this.ProductId = productId;
            this.ProductName = productName;
            if (ProductName.StartsWith("Bulk"))
            {
                this.MinimumPrice = 9.99m;
            }

            Console.WriteLine("Product instance has a name: " + ProductName);
        }


        #endregion Constructors

        #region Properties
        private DateTime? availabilityDate; // type sonuna "?" oyarsan nullable type olur

        public DateTime? AvailabilityDate
        {
            get { return availabilityDate; }
            set { availabilityDate = value; }
        }

        public decimal Cost { get; set; }

        private string productName;

        public string ProductName
        {
            get {
                var formattedValue = productName?.Trim();
                return formattedValue;
            }
            set
            {
                if (value.Length < 3)
                {
                    ValidationMessage = "Product Name must be at least three characters";
                }
                else if (value.Length > 20)
                {
                    ValidationMessage = "Product Name cannot be more than 20 characters";
                }
                else
                {
                    productName = value;
                }


            }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private int productId;

        public int ProductId
        {
            get => productId;
            set { productId = value; }
        }

        private Vendor productVendor;

        public Vendor ProductVendor
        {
            get {     // lazy loading "sometimes" needed olan durumlar icin kullanılacak Relted Object Initialization stayla..
                if (productVendor == null)
                {
                    productVendor = new Vendor();
                }
                return productVendor; }
            set { productVendor = value; }
        }

        public string ValidationMessage { get; private set; } //Accesibility modifier in get/ set must be more restrictive than the property (try it out)

        internal string Category { get; set; } = "Tools";
        public int SequenceNumber { get; set; } = 1;

        // public string ProductCode => this.Category + "-" + this.SequenceNumber;  // c# 6 nın özeliği olan LAMBDA op

        // yukardaki this.Category + "-" + this.SequenceNumber; ifadesinin farklı bir kullanım şekli aşagıdadır

        // public string ProductCode => String.Format("{0}-{1}", this.Category, this.SequenceNumber);  // Formatting Strings in c#6
        // but this type of using is bulky , hataya meğilli bunun yerine string interpolation dedikleri yonteme bakalım:
        public string ProductCode => $"{this.Category}-{this.SequenceNumber}"; //for using string interpolation (only in c#6) put the "$" sign in front of the string code 
        

        #endregion Properties


        //  Calculates the suggested retail price //with expression - bodied method syntax (lambda op)
        public decimal CalculateSuggestedPrice(decimal markupPercent) => this.Cost + (this.Cost * markupPercent / 100);
        

        public string SayHello()    
        {
            // var vendor = new Vendor();
            // vendor.SendWelcomeEmail("messsage from Product");


            var emailService = new EmailService(); //farklı class oldugundan bagli oldugu class using ile implement edildi.
            emailService.SendMessage("New Product", this.ProductName, "sales@abc.com");

            var result = LoggingService.LogAction("saying hello");
            //this.MinimumPrice = 7m; redonly oldugundan şu hatayı verir: A readonly field cannot be assigned to (except constructor or object initializatior 
            return "Hello " + ProductName + " (" + ProductId + "): " + Description + "Available on: " + AvailabilityDate?.ToShortDateString(); // c#6 null checking op
        }

        public override string ToString()    //overriding ToString
        {
            return this.ProductName + "(" + this.productId + ")";
        }
    }
}
