using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Partner.Comms.DTO
{
  
    // This is the message that should be put into Email Topic
    public partial class PartnerEmailDTO    {
        [JsonProperty("TggPartnerMsgId")]
        public string TggPartnerMsgId { get; set; }

        [JsonProperty("_data")]
        public EmailDetails Info { get; set; }
    }

    public partial class EmailDetails
    {
        //   public Customer Customer { get; set; }

        [JsonProperty("aet_customer")]
        public List<Customer> Customer { get; set; }
    }

    public partial class Customer
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("userid")]
        public string UserId { get; set; }

        [JsonProperty("fname")]
        public string FirstName { get; set; }

        [JsonProperty("lname")]
        public string LastName { get; set; }

        [JsonProperty("customeremail")]
        public string CustomerEmail { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("shiptofirstname")]
        public string ShipToFirstName { get; set; }

        [JsonProperty("shiptolastname")]
        public string ShipToLastName { get; set; }

        [JsonProperty("shippingaddress")]
        public string ShippingAddress { get; set; }

        [JsonProperty("shiptocity")]
        public string ShipToCity { get; set; }

        [JsonProperty("shiptostate")]
        public string ShipToState { get; set; }

        [JsonProperty("shiptozipcode")]
        public string ShipToZipcode { get; set; }

        [JsonProperty("shiptodayphone")]
        public string ShipTodayPhone { get; set; }

        [JsonProperty("shiptoemailid")]
        public string ShipToEmailID { get; set; }


        // TODO: only add this if customer phone number exists
        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("billingtitle")]
        public string BillingTitle { get; set; }

        [JsonProperty("billingfname")]
        public string BillingFirstName { get; set; }

        [JsonProperty("billinglname")]
        public string BillingLastName { get; set; }

        [JsonProperty("billingaddress")]
        public string BillingAddress { get; set; }

        [JsonProperty("billingemail")]
        public string BillingEmail { get; set; }

        [JsonProperty("billingphone")]
        public string BillingPhone { get; set; }

        [JsonProperty("buyeremail")]
        public string BuyerEmail { get; set; }

        [JsonProperty("entrytype")]
        public string EntryType { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("organisationname")]
        public string OrganisationName { get; set; }

        [JsonProperty("organisationaddress")]
        public string OrganisationAddress { get; set; }

        [JsonProperty("organisationemail")]
        public string OrganisationEmail { get; set; }

        [JsonProperty("organisationmobile")]
        public string OrganisationMobile { get; set; }

        [JsonProperty("organisationphone")]
        public string OrganisationPhone { get; set; }

        [JsonProperty("pickupstatus")]
        public string PickupStatus { get; set; }

        [JsonProperty("plpcdiscount")]
        public string PlpcDiscount { get; set; }

        [JsonProperty("quote")]
        public string Quote { get; set; }

        [JsonProperty("resetpasswordlink")]
        public string ResetPasswordLink { get; set; }

        [JsonProperty("hide_reset_password_link")]
        public string HideResetPasswordLink { get; set; }

        [JsonProperty("storename")]
        public string StoreName { get; set; }

        [JsonProperty("storephone")]
        public string StorePhone { get; set; }

        [JsonProperty("storeaddress")]
        public string StoreAddress { get; set; }

        [JsonProperty("storeabn")]
        public string StoreAbn { get; set; }

        [JsonProperty("storeacn")]
        public string StoreAcn { get; set; }

        [JsonProperty("storefax")]
        public string StoreFax { get; set; }

        [JsonProperty("storebillercode")]
        public string StoreBillerCode { get; set; }

        [JsonProperty("storereferencenumber")]
        public string StoreReferenceNumber { get; set; }

        [JsonProperty("storeemailid")]
        public string StoreEmailID { get; set; }

        [JsonProperty("ereceipt")]
        public string EReceipt { get; set; }

        [JsonProperty("pay_now_url")]
        public string PayNowUrl { get; set; }

        [JsonProperty("deliverydate")]
        public string DeliveryDate { get; set; }

        [JsonProperty("aet_order_header.aetorderheadertoaetcustomer")]
        public List<OrderHeaderCustomer> OrderHeaderCustomerDetails { get; set; }

        [JsonProperty("aet_homeservice_header.aethomeserviceheadertoaetcustomer")]
        public List<HomeServiceHeaderCustomer> HomeServiceHeaderCustomer { get; set; }
    }

    public partial class HomeServiceHeaderCustomer
    {
        [JsonProperty("workorderno")]
        public string WorkOrderNo { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("service_date")]
        public string ServiceDate { get; set; }

        [JsonProperty("service_completion_date")]
        public string ServiceCompletionDate { get; set; }

        [JsonProperty("service_totalprice")]
        public string ServiceTotalprice { get; set; }

        [JsonProperty("service_gst")]
        public string ServiceGst { get; set; }

        [JsonProperty("service_totalpaid")]
        public string ServiceTotalPaid { get; set; }

        [JsonProperty("contractor_fname")]
        public string ContractorFirstName { get; set; }

        [JsonProperty("contractor_lname")]
        public string ContractorLastName { get; set; }

        [JsonProperty("contractor_company")]
        public string ContractorCompany { get; set; }

        [JsonProperty("contractor_phone")]
        public string ContractorPhone { get; set; }

        [JsonProperty("quote_name")]
        public string QuoteName { get; set; }

        [JsonProperty("quote_description")]
        public string QuoteDescription { get; set; }

        [JsonProperty("product_code")]
        public string ProductCode { get; set; }

        [JsonProperty("product_name")]
        public string ProductName { get; set; }

        [JsonProperty("product_price")]
        public string ProductPrice { get; set; }

        [JsonProperty("product_qty")]
        public long ProductQty { get; set; }

        [JsonProperty("product_description")]
        public string ProductDescription { get; set; }

        [JsonProperty("delivery_vendor")]
        public string DeliveryVendor { get; set; }

        [JsonProperty("delivery_model")]
        public string DeliveryModel { get; set; }

        [JsonProperty("delivery_description")]
        public string DeliveryDescription { get; set; }

        [JsonProperty("delivery_status")]
        public string DeliveryStatus { get; set; }

        [JsonProperty("delivery_location")]
        public string DeliveryLocation { get; set; }

        [JsonProperty("delivery_date")]
        public string DeliveryDate { get; set; }

        [JsonProperty("optional1")]
        public string Optional1 { get; set; }

        [JsonProperty("optional2")]
        public string Optional2 { get; set; }

        [JsonProperty("aet_homeservice_details.aethsdetailstoaethsheader")]
        public List<HomeServiceDetailsHeader> HomeServiceDetails { get; set; }
    }

    public partial class HomeServiceDetailsHeader
    {
        [JsonProperty("workorderno")]
        public string WorkOrderNo { get; set; }

        [JsonProperty("itemid")]
        public string ItemID { get; set; }

        [JsonProperty("attribute_name")]
        public string AttributeName { get; set; }

        [JsonProperty("attribute_value")]
        public string AttributeValue { get; set; }

        [JsonProperty("service_additions_name")]
        public string ServiceAdditionsName { get; set; }

        [JsonProperty("service_additions_price")]
        public string ServiceAdditionsPrice { get; set; }

        [JsonProperty("service_additions_qty")]
        public long ServiceAdditionsQty { get; set; }

        [JsonProperty("prebook_question")]
        public string PrebookQuestion { get; set; }

        [JsonProperty("prebook_response")]
        public string PrebookResponse { get; set; }

        [JsonProperty("prebook_warning")]
        public string PrebookWarning { get; set; }

        [JsonProperty("travel_price")]
        public string TravelPrice { get; set; }

        [JsonProperty("travel_name")]
        public string TravelName { get; set; }

        [JsonProperty("travel_qty")]
        public long TravelQty { get; set; }

        [JsonProperty("optional1")]
        public string Optional1 { get; set; }

        [JsonProperty("optional2")]
        public string Optional2 { get; set; }
    }

    public partial class OrderHeaderCustomer
    {
        [JsonProperty("orderid")]
        public string OrderID { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("orderdate")]
        public string OrderDate { get; set; }

        [JsonProperty("ordertotal")]
        public long OrderTotal { get; set; }

        [JsonProperty("ordertype")]
        public string OrderType { get; set; }

        [JsonProperty("delivery")]
        public string Delivery { get; set; }

        [JsonProperty("deliverycharge")]
        public long DeliveryCharge { get; set; }

        [JsonProperty("deliverydescription")]
        public string DeliveryDescription { get; set; }

        [JsonProperty("deliverytype")]
        public string DeliveryType { get; set; }

        [JsonProperty("subtotal")]
        public long SubTotal { get; set; }

        [JsonProperty("deliverydate")]
        public string DeliveryDate { get; set; }

        [JsonProperty("totalpayment")]
        public long TotalPayment { get; set; }

        [JsonProperty("totalprice")]
        public long TotalPrice { get; set; }

        [JsonProperty("deposit")]
        public long Deposit { get; set; }

        [JsonProperty("outstandingbalance")]
        public long OutstandingBalance { get; set; }

        [JsonProperty("weborderno")]
        public string WebOrderNo { get; set; }

        [JsonProperty("docketno")]
        public string DocketNo { get; set; }

        [JsonProperty("salesperson")]
        public string SalesPerson { get; set; }

        [JsonProperty("salesorderno")]
        public string SalesOrderNo { get; set; }

        [JsonProperty("goodsdispatched")]
        public string GoodsDispatched { get; set; }

        [JsonProperty("securitydeposit")]
        public string SecurityDeposit { get; set; }

        [JsonProperty("cashondelivery")]
        public string CashOnDelivery { get; set; }

        [JsonProperty("gst")]
        public string Gst { get; set; }

        [JsonProperty("tender")]
        public string Tender { get; set; }

        [JsonProperty("tender2")]
        public string Tender2 { get; set; }

        [JsonProperty("change")]
        public string Change { get; set; }

        [JsonProperty("goodsonorder")]
        public string GoodsOnOrder { get; set; }

        [JsonProperty("goodstaken")]
        public string GoodsTaken { get; set; }

        [JsonProperty("totaltendered")]
        public string TotalTendered { get; set; }

        // TODO : Changes - earlier it was depositheld
        [JsonProperty("securitydepositheld")]
        public string SecurityDepositHeld { get; set; }

        // TODO: DocketTotal wasn't even there
        [JsonProperty("dockettotal")]
        public string DocketTotal { get; set; }

        [JsonProperty("totaldepositsapplied")]
        public string TotalDepositsApplied { get; set; }

        [JsonProperty("aet_order_details.aetorderdetailstoaetorderheader")]
        public List<OrderDetailsHeader> OrderDetailsHeader { get; set; }

        [JsonProperty("aet_payment.aetpaymenttoaetorderheader")]
        public List<PaymentOrderHeader> PaymentOrderHeader { get; set; }
    }

    public partial class OrderDetailsHeader
    {
        [JsonProperty("orderid")]
        public string OrderID { get; set; }

        [JsonProperty("line_number")]
        public string LineNumber { get; set; }

        [JsonProperty("itemid")]
        public string ItemID { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("manufacturername")]
        public string ManufacturerName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("productid")]
        public string ProductID { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("totalprice")]
        public long TotalPrice { get; set; }

        [JsonProperty("availability")]
        public string Availability { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("modelno")]
        public string ModelNo { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }

        [JsonProperty("unitprice")]
        public long UnitPrice { get; set; }

        [JsonProperty("purchasetype")]
        public string PurchaseType { get; set; }

        [JsonProperty("sku")]
        public string SKU { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("storeid")]
        public string StoreID { get; set; }

        [JsonProperty("storecity")]
        public string StoreCity { get; set; }

        [JsonProperty("storestate")]
        public string StoreState { get; set; }

        [JsonProperty("storedayphone")]
        public string StoreDayPhone { get; set; }

        [JsonProperty("sin")]
        public string Sin { get; set; }

        [JsonProperty("mths")]
        public string Mths { get; set; }

        [JsonProperty("bay")]
        public string Bay { get; set; }

        [JsonProperty("digital_key")]
        public string DigitalKey { get; set; }

        [JsonProperty("digital")]
        public string Digital { get; set; }

        [JsonProperty("aet_order_instructions.aetorderinstructionstoorderdetails")]
        public List<OrderInstructionsOrderDetails> OrderInstructionsOrderDetails { get; set; }

        [JsonProperty("aet_service.aetservicetoaetorderdetails")]
        public List<ServiceOrderDetails> ServiceOrderDetails { get; set; }

        [JsonProperty("aet_warranty.aetwarrantytoaetorderdetails")]
        public List<WarrantyOrderDetails> WarrantyOrderDetails { get; set; }
    }

    public partial class OrderInstructionsOrderDetails
{
        [JsonProperty("itemid")]
        public string ItemID { get; set; }

        [JsonProperty("lineid")]
        public string LineID { get; set; }

        [JsonProperty("instruction")]
        public string Instruction { get; set; }
    }

    public partial class ServiceOrderDetails
{
        [JsonProperty("itemid")]
        public string ItemID { get; set; }

        [JsonProperty("serviceid")]
        public string ServiceID { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }
    }

    public partial class WarrantyOrderDetails
    {
        [JsonProperty("itemid")]
        public string ItemID { get; set; }

        [JsonProperty("warrantyid")]
        public string WarrantyID { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("warrantyinfo")]
        public string Warrantyinfo { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }
    }

    public partial class PaymentOrderHeader
    {
        [JsonProperty("orderid")]
        public string OrderID { get; set; }

        [JsonProperty("paymentid")]
        public long PaymentID { get; set; }

        [JsonProperty("paymentamount")]
        public long PaymentAmount { get; set; }

        [JsonProperty("paymentmethod")]
        public string PaymentMethod { get; set; }

        [JsonProperty("paymentreference")]
        public string PaymentReference { get; set; }

        [JsonProperty("paymenttype")]
        public string PaymentType { get; set; }
    }


}

