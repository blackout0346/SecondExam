using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore;
using secondExam.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace secondExam
{

    class Productss
    {

        [Name("Наименование продукции")]
        public string Name { get; set; }


        [Name("Артикул")]

        public int Article { get; set; }
        [Name("Минимальная стоимость для партнера")]

        public decimal MinPriceForPartner { get; set; }

        [Name("Тип продукции")]
        public string typeproducts { get; set; }
    }

    class PartnerProductss
    {
        [Name("Продукция")]
        public string ProductName { get; set; }
        [Name("Наименование партнера")]
        public string NamePartner { get; set; }
        [Name("Количество продукции")]
        public int CountProducts { get; set; }

        [Name("Дата продажи")]
        public DateTime DateSale { get; set; }

    }
    class TypeProducts
    {



        [Name("Тип продукции")]
        public string name { get; set; }
        [Name("Коэффициент типа продукции")]
        public decimal Discount { get; set; }




    }
    class TypePartnersa
    {
        [Name("Тип партнера")]
        public string NamePartner { get; set; }

        [Name("Наименование партнера")]
        public string NamePartnerPlace { get; set; }
        [Name("Директор")]
        public string DirectionName { get; set; }
        [Name("Телефон партнера")]
        public string number { get; set; }
        [Name("Электронная почта партнера")]
        public string email { get; set; }
        [Name("Юридический адрес партнера")]
        public string URPartner { get; set; }
        [Name("ИНН")]
        public string INN { get; set; }
        [Name("Рейтинг")]
        public int Rate { get; set; }


    }
    class TypeMaterials
    {
        [Name("Тип материала")]
        public string TypeProduction { get; set; }

        [Name("Процент брака материала")]
        public decimal Discount { get; set; }

    }
    class ParseFromCSV
    {
        AppDbContext db;
        TypeProduct TypeProduts;
        TypePartner TypePartners;
        Partner Partners;
        Products Productss;

        public ParseFromCSV(AppDbContext context)
        {
            db = context;
        }
        public void csvAdding()
        {
         
       
            using (var reader = new StreamReader("Material_type_import.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var data = csv.GetRecords<TypeMaterials>().ToList();
                    foreach (var item in data)
                    {
                        Debug.WriteLine($"{item.TypeProduction}, {item.Discount}");
                        var typeMaterial = new TypeMaterial() { material = item.TypeProduction, Break = item.Discount };
                        db.Add(typeMaterial);
                        db.SaveChanges();
                    }

                }
            }

        }


        public void csvAddingPartner()
        {
            var typePartners = new TypePartner();
            this.TypePartners = typePartners;
            var partner = new Partner();

            Partners = partner;
       
  
            using (var reader = new StreamReader("Partners_import - Partners_import.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var data = csv.GetRecords<TypePartnersa>().ToList();
                    foreach (var item in data)
                    {
                        Debug.WriteLine($"{item.NamePartner}");
                        TypePartners = new TypePartner() { NamePartner = item.NamePartner };
                        Partners = new Partner() { NameParntersPlace = item.NamePartnerPlace, DirectionName = item.DirectionName,number=item.number ,email = item.email, URPartner=item.URPartner, INN = item.INN, Rate = item.Rate, typePartners = TypePartners };
                        db.AddRange(TypePartners, Partners);
                        db.SaveChanges();
                    }

                }
            }

        }
        public void csvAddingtypeProduct()
        {
            var typeProduts = new TypeProduct();
            this.TypeProduts = typeProduts;
          
    

            using (var reader = new StreamReader("Product_type_import - Product_type_import.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var data = csv.GetRecords<TypeProducts>().ToList();
                    foreach (var item in data)
                    {
                        Debug.WriteLine($"{item.name}, {item.Discount}");

                        this.TypeProduts = new TypeProduct() { Name = item.name, Discount = item.Discount };
                        db.Add(this.TypeProduts);
                        db.SaveChanges();
                    }

                }
            }

        }
        public void csvAddingProducts()
        {
          
            var products = new Products();
            Productss = products;
  
            using (var reader = new StreamReader("Products_import - Products_import.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var data = csv.GetRecords<Productss>().ToList();
                    foreach (var item in data)
                    {

                        var type = db.TypeProduct.FirstOrDefault(f => f.Name == item.typeproducts);
                        if (type != null)
                        {
                            var exesting = db.Products.FirstOrDefault(p => p.typeProduct.Id == type.Id);
                            if(exesting == null)
                            {
                                Productss = new Products() { name = item.Name, Article = item.Article, MinPriceForPartner = item.MinPriceForPartner, typeProduct = type };

                                db.Add(Productss);
                            }
                        }
                       
                        db.SaveChanges();
                    }

                }
            }

        }
        public void csvAddingPartnerProductsa()
        {

        

            using (var reader = new StreamReader("Partner_products_import - Partner_products_import.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var data = csv.GetRecords<PartnerProductss>().ToList();
                    foreach (var item in data)
                    {
                        var partner = db.Partner.FirstOrDefault(p => p.NameParntersPlace == item.NamePartner);
                        
                        var product = db.Products.FirstOrDefault(p => p.name == item.ProductName);
                        if (partner != null && product != null)
                        {
                            var exesting = db.PartnerProducts.FirstOrDefault(p => p.Partner.Id == partner.Id && p.Product.Id == product.Id && p.DateSale == item.DateSale);
                            if (exesting == null)
                            {
                                var PartnerProduct = new PartnerProducts() { CountProducts = item.CountProducts, Partner = partner, DateSale = item.DateSale, Product = product };

                                db.Add(PartnerProduct);
                            }

                        }
                        //Debug.WriteLine($"{item.TypeProduction}, {item.Discount}");
           
                        db.SaveChanges();
                    }

                }
            }

        }
    }
}
