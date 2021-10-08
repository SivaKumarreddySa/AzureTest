using AutoMapper;
using Partner.Comms.Domain.Docket;
using Partner.Comms.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Partner.Comms.PayLink.FuncApp
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(
            )
        {
            CreateMaps();
        }

        private void CreateMaps()
        {



            CreateMap<CommunicationDTO, Partner.Comms.DTO.PartnerEmailDTO>()
                .ForMember(dest => dest.TggPartnerMsgId, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_uid))
                .AfterMap((src, dest, context) =>
                {
                    var customer = context.Mapper.Map<Partner.Comms.DTO.EmailDetails>(src);
                    dest.Info = customer;
                }
                );

            CreateMap<CommunicationDTO, Partner.Comms.DTO.EmailDetails>()
                 .AfterMap((src, dest, context) =>
                 {
                         dest.Customer = new List<Partner.Comms.DTO.Customer>();
                         var customer = context.Mapper.Map<Partner.Comms.DTO.Customer>(src);
                         dest.Customer.Add(customer);
                 }
                );


            CreateMap<CommunicationDTO, Partner.Comms.DTO.Customer>()
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.paylinkDTO.EmailAddress))
              .ForMember(dest => dest.Title, opt => opt.MapFrom(src => ""))

              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.cust_nbr))
              .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.customer.cust_first_name))
             
              .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.customer.cust_surname))
               .ForMember(dest => dest.CustomerEmail, opt => opt.Ignore())

               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.customer.address.addr1))
               .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.customer.address.addr_city))
                  .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.customer.address.addr_state))
               .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.customer.address.post_code))
               // Pending Country
             //.ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.customer.address.addr1))

             // TODO: Add <mobile> element in xml if customer mobile number exists
             //  .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.customer.address.addr1))


             // TODO:  Phone - should this be mapped to Customer.Address.Phone_nbr or whatever phone number we are including in the SMS after running paylink rules?
             //.ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.customer.address.phone_nbr))

             .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.store_details.descr))

             .ForMember(dest => dest.StorePhone, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.store_details.phone_nbr))

              /*TODO: StoreAddress - 
               * if StoreName=WEB, then concat StoreAddress +' '+ StoreState +' '+ StorePostCode
                     else concat StoreAddress +' ' + StoreName + ' ' + StoreState +' '+ StorePostCode
              - May have to move above into AfterMap below             
              */
              // Confirm with Sigi
              .ForMember(dest => dest.StoreAddress, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.store_details.addr1 + " " +
                      src.pOSDocketDTO.dkt_details.store_details.addr2 + " " +
                      src.pOSDocketDTO.dkt_details.store_details.addr_city + " " +
                      src.pOSDocketDTO.dkt_details.store_details.addr_state + " " + src.pOSDocketDTO.dkt_details.store_details.post_code))

             
              .ForMember(dest => dest.StoreAbn, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.store_details.locn_business_nbr))
              // Pending StoreAcn
              // .ForMember(dest => dest.StoreAcn, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.customer.address.AB))

              .ForMember(dest => dest.StoreFax, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.store_details.fax_nbr))

             // TODO: StoreBillerCode - What is this mapped to in the docket/docket store details?
             //.ForMember(dest => dest.StoreBillerCode, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.store_details.fax_nbr))

             // TODO: storereferencenumber - what is this mapped to in the docket/docket store details?
             //.ForMember(dest => dest.StoreReferenceNumber, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.store_details.))

             .ForMember(dest => dest.StoreEmailID, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.store_details.email))

             // TODO: EReceipt - Not available
             //.ForMember(dest => dest.EReceipt, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.store_details.ec))

             .ForMember(dest => dest.PayNowUrl, opt => opt.MapFrom(src => src.paylinkDTO.LongPayLink))

             .AfterMap((src, dest, context) => {
                 dest.OrderHeaderCustomerDetails = new List<Partner.Comms.DTO.OrderHeaderCustomer>();
                 var orderHeaderCustomer = context.Mapper.Map<Partner.Comms.DTO.OrderHeaderCustomer>(src);
                 dest.OrderHeaderCustomerDetails.Add(orderHeaderCustomer);


                 dest.HomeServiceHeaderCustomer = new List<Partner.Comms.DTO.HomeServiceHeaderCustomer>();
                 var homeServiceCustomer = context.Mapper.Map<Partner.Comms.DTO.HomeServiceHeaderCustomer>(src);
                 dest.HomeServiceHeaderCustomer.Add(homeServiceCustomer);
             });

            CreateMap<CommunicationDTO, Partner.Comms.DTO.OrderHeaderCustomer>()
                .ForMember(dest => dest.OrderID, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.dkt_nbr))

                // TODO : Customer Email and email under order head to customer - we are including paylink email(if it exists)
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.paylinkDTO.EmailAddress))

                // TODO : Customer Email and email under order head to customer - we are including paylink email(if it exists)
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.trans_datei))

                .ForMember(dest => dest.OrderTotal, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.doc_tot_amt))
                
                .ForMember(dest => dest.DeliveryDescription, opt => opt.MapFrom(src => $"Channel {src.pOSDocketDTO.dkt_details.ChannelID}"))

                // TODO : totaldepositsapplied - Is this same as totaldepositsapplied under dkt_details?
                //.ForMember(dest => dest.TotalDepositsApplied, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.))

                .ForMember(dest => dest.OutstandingBalance, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.doc_bal_amt))

                .ForMember(dest => dest.WebOrderNo, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.web_order_nbr))
                .ForMember(dest => dest.DocketNo, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.dkt_nbr))

                .ForMember(dest => dest.SalesPerson, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.sales_person))
                .ForMember(dest => dest.SalesOrderNo, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.sales_order))


                // Pending GoodsDispatched,cashondelivery
                //.ForMember(dest => dest.GoodsDispatched, opt => opt.MapFrom(src => src.pOSDocketDTO.))
                //.ForMember(dest => dest.CashOnDelivery, opt => opt.MapFrom(src => src.pOSDocketDTO.))
                .ForMember(dest => dest.SecurityDeposit, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.securitydeposit))
                .ForMember(dest => dest.Gst, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.total_tax_amt))

                // TODO : In xml conversion, include this element only if the value exists
                .ForMember(dest => dest.Tender2, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.tender_amt))

                // TODO : change is mapped to change_amt
                .ForMember(dest => dest.Change, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.change_amt))
                .ForMember(dest => dest.GoodsOnOrder, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.goodsonorder))


                 // TODO : goodstaken, totaltendered, securitydepositheld
                 // .ForMember(dest => dest.GoodsTaken, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details))
                 .ForMember(dest => dest.SecurityDepositHeld, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.securitydeposit))

                 .ForMember(dest => dest.DocketTotal, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.total_amt))
                    .AfterMap((src, dest, context) => {
                        dest.OrderDetailsHeader = new List<Partner.Comms.DTO.OrderDetailsHeader>();
                        //var orderDetailHeader = context.Mapper.Map<Partner.Comms.DTO.OrderDetailsHeader>(src);
                        //dest.OrderDetailsHeader.Add(orderDetailHeader);
                        var orderDetailHeader = context.Mapper.Map<List<Partner.Comms.DTO.OrderDetailsHeader>>(src.pOSDocketDTO.dkt_details.lines);
                        //dest.PaymentOrderHeader = new List<Partner.Comms.DTO.PaymentOrderHeader>();
                        //var paymentOrderHeader = context.Mapper.Map<Partner.Comms.DTO.PaymentOrderHeader>(src);
                        //dest.PaymentOrderHeader.Add(paymentOrderHeader);
                        dest.OrderDetailsHeader = orderDetailHeader.Select(x => { x.OrderID = src.pOSDocketDTO.dkt_details.dkt_nbr; return x; }).ToList();
                    });

            CreateMap<Line, Partner.Comms.DTO.OrderDetailsHeader>()
                 .ForMember(dest => dest.LineNumber, opt => opt.MapFrom(src => src.dkt_lne_nbr))                 
                 .ForMember(dest => dest.ItemID, opt => opt.MapFrom(src => src.prod_details.prod_nbr))

                 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.prod_details.product_descr))

                 .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.trans_qty))

                 // TODO: Should this be total_line_amt + total_line_vat?
                 .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.total_line_amt))

                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.prod_details.brand))

                .ForMember(dest => dest.ModelNo, opt => opt.MapFrom(src => src.prod_details.sup_prod_nbr))

                 // TODO : What should it be? Following should be done
                 //.ForMember(dest => dest.SKU, opt => opt.MapFrom(src => src.))
                 //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.))
                 //.ForMember(dest => dest.Sin, opt => opt.MapFrom(src => src.))
                 .ForMember(dest => dest.Mths, opt => opt.MapFrom(src => "12"))
                 //.ForMember(dest => dest.Bay, opt => opt.MapFrom(src => src.))

                 .AfterMap((src, dest, context) => {
                     dest.ServiceOrderDetails = new List<Partner.Comms.DTO.ServiceOrderDetails>();
                     var serviceOrderDetail = context.Mapper.Map<Partner.Comms.DTO.ServiceOrderDetails>(src);
                     //dest.ServiceOrderDetails.Add(serviceOrderDetail);

                     dest.WarrantyOrderDetails = new List<Partner.Comms.DTO.WarrantyOrderDetails>();
                     var warrantyOrderDetail = context.Mapper.Map<Partner.Comms.DTO.WarrantyOrderDetails>(src);
                     dest.WarrantyOrderDetails.Add(warrantyOrderDetail);

                     dest.OrderInstructionsOrderDetails = new List<Partner.Comms.DTO.OrderInstructionsOrderDetails>();
                     var instructionsOrderDetail = context.Mapper.Map<Partner.Comms.DTO.OrderInstructionsOrderDetails>(src);
                     dest.OrderInstructionsOrderDetails.Add(instructionsOrderDetail);


                 });

            ;



            //// Use below lines only to map final message object to OrderDetailsHeader
            //CreateMap<CommunicationDTO, Partner.Comms.DTO.OrderDetailsHeader>()
            //  .AfterMap((src, dest, context) => {
            //      dest.OrderInstructionsOrderDetails = new List<Partner.Comms.DTO.OrderInstructionsOrderDetails>();
            //      var instructionsOrderDetail = context.Mapper.Map<Partner.Comms.DTO.OrderInstructionsOrderDetails>(src);
            //      dest.OrderInstructionsOrderDetails.Add(instructionsOrderDetail);

            //      dest.ServiceOrderDetails = new List<Partner.Comms.DTO.ServiceOrderDetails>();
            //      var serviceOrderDetail = context.Mapper.Map<Partner.Comms.DTO.ServiceOrderDetails>(src);
            //      dest.ServiceOrderDetails.Add(serviceOrderDetail);

            //      dest.WarrantyOrderDetails = new List<Partner.Comms.DTO.WarrantyOrderDetails>();
            //      var warrantyOrderDetail = context.Mapper.Map<Partner.Comms.DTO.WarrantyOrderDetails>(src);
            //      dest.WarrantyOrderDetails.Add(warrantyOrderDetail);
            //  });

            CreateMap<Line, Partner.Comms.DTO.ServiceOrderDetails>();

            CreateMap<Line, Partner.Comms.DTO.WarrantyOrderDetails>();
            CreateMap<Line, Partner.Comms.DTO.OrderInstructionsOrderDetails>();
            CreateMap<Line, Partner.Comms.DTO.PaymentOrderHeader>();

            //   Use below 4 lines only to map main message to child objects

            //CreateMap<CommunicationDTO, Partner.Comms.DTO.ServiceOrderDetails>();
            //CreateMap<CommunicationDTO, Partner.Comms.DTO.WarrantyOrderDetails>();
            //CreateMap<CommunicationDTO, Partner.Comms.DTO.OrderInstructionsOrderDetails>();
            // CreateMap<CommunicationDTO, Partner.Comms.DTO.PaymentOrderHeader>();

            CreateMap<CommunicationDTO, Partner.Comms.DTO.HomeServiceHeaderCustomer>()
             .ForMember(dest => dest.WorkOrderNo, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.web_order_nbr))
            // .ForMember(dest => dest.QuoteName, opt => opt.MapFrom(src => src.pOSDocketDTO.dkt_details.web_order_nbr))

          .AfterMap((src, dest, context) => {
              dest.HomeServiceDetails = new List<Partner.Comms.DTO.HomeServiceDetailsHeader>();
              var homeServiceDetail = context.Mapper.Map<Partner.Comms.DTO.HomeServiceDetailsHeader>(src);
              dest.HomeServiceDetails.Add(homeServiceDetail);
          });

            CreateMap<CommunicationDTO, Partner.Comms.DTO.HomeServiceDetailsHeader>();



        }
    }
}