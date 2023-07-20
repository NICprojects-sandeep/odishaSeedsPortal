PGDMP     (    8                {            stock    15.3    15.3 J    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    33473    stock    DATABASE     x   CREATE DATABASE stock WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_India.1252';
    DROP DATABASE stock;
                postgres    false            �            1255    33572    hello_world() 	   PROCEDURE     �   CREATE PROCEDURE public.hello_world()
    LANGUAGE plpgsql
    AS $$
BEGIN
INSERT INTO public."Stock_District"(
	"Dist_Code", "Dist_Name", "NDist_Code", "LGDistrict")
	VALUES ('345', 'bargarh', '345', 14);
END
$$;
 %   DROP PROCEDURE public.hello_world();
       public          postgres    false            �            1255    33563 
   transfer() 	   PROCEDURE     �   CREATE PROCEDURE public.transfer()
    LANGUAGE plpgsql
    AS $$
begin
    -- subtracting the amount from the sender's account 
  select * from public."Stock_District"

    commit;
end;
$$;
 "   DROP PROCEDURE public.transfer();
       public          postgres    false            �            1259    41730    ERROR_CATCH    TABLE     �  CREATE TABLE public."ERROR_CATCH" (
    "ERROR_NUMBER" character varying(50),
    "ERROR_SEVERITY" character varying(50),
    "ERROR_STATE" character varying(50),
    "ERROR_PROCEDURE" character varying(50),
    "ERROR_LINE" character varying(50),
    "ERROR_MESSAGE]" character varying,
    "UPDATED_ON" timestamp with time zone,
    "USER_IP" character varying(50),
    "UPDATED_BY" character varying(50)
);
 !   DROP TABLE public."ERROR_CATCH";
       public         heap    postgres    false            �            1259    41735    STOCK_BAGSIZE    TABLE     �   CREATE TABLE public."STOCK_BAGSIZE" (
    "BAG_SIZE" integer NOT NULL,
    "BAG_NAME" character varying(50),
    "IS_ACTIVE" bigint
);
 #   DROP TABLE public."STOCK_BAGSIZE";
       public         heap    postgres    false            �            1259    41740    STOCK_DEALERSTOCK    TABLE     b  CREATE TABLE public."STOCK_DEALERSTOCK" (
    "STOCK_ID" integer NOT NULL,
    "LICENCE_NO" character varying(50) NOT NULL,
    "CLASS" character varying(50) NOT NULL,
    "RECEIVE_UNITCD" character varying(4) NOT NULL,
    "MOU_REFNO" character varying(50),
    "CROPCATG_ID" character varying(2),
    "CROP_VERID" character varying(4) NOT NULL,
    "CROP_ID" character varying(4) NOT NULL,
    "SEASSION" character varying(50) NOT NULL,
    "FIN_YR" character varying(7) NOT NULL,
    "LOT_NO" character varying(50) NOT NULL,
    "BAG_SIZE_IN_KG" character varying(10) NOT NULL,
    "RECV_NO_OF_BAGS" integer,
    "AVL_NO_OF_BAGS" integer,
    "PRICE_QTL" numeric(10,2) NOT NULL,
    "SUBSIDY_QTL" numeric(10,2),
    "STOCK_DATE" timestamp with time zone,
    "STOCK_QUANTITY" numeric(10,2),
    "AVL_QUANTITY" numeric(10,2),
    "USER_TYPE" character varying(50),
    "ENTRYDATE" timestamp with time zone,
    "USERID" character varying(50),
    "USERIP" character varying(50),
    "ENTRY_STATUS" bigint,
    "TESTING_DATE" timestamp with time zone,
    "EXPIRY_DATE" timestamp with time zone,
    "VALIDITY" bigint
);
 '   DROP TABLE public."STOCK_DEALERSTOCK";
       public         heap    postgres    false            �            1259    41745    STOCK_FARMER    TABLE     q  CREATE TABLE public."STOCK_FARMER" (
    "FARMER_ID" character varying(50) NOT NULL,
    "TRANSACTION_ID" character varying(50) NOT NULL,
    "CROPCATG_ID" character varying(2),
    "CROP_ID" character varying(4),
    "CROP_VERID" character varying(4),
    "CROP_CLASS" character varying(50),
    "Receive_Unitcd" character varying(50),
    "LOT_NUMBER" character varying(50) NOT NULL,
    "BAG_SIZE_KG" integer,
    "NO_OF_BAGS" integer,
    "TOT_QTL" numeric(10,2),
    "ADMISSIBLE_SUBSIDY" numeric(10,2),
    "PRICE_QTL" numeric(10,2),
    "ALL_IN_COST_AMOUNT" numeric(10,2),
    "SCHEME_CODE_GOI" character varying(50),
    "TOT_SUB_AMOUNT_GOI" numeric(10,2),
    "SCHEME_CODE_SP" character varying(50),
    "TOT_SUB_AMOUNT_SP" numeric(10,2),
    "SUBSIDY_AMOUNT" numeric(10,2),
    "SEASON" character varying(50) NOT NULL,
    "FIN_YEAR" character varying(7) NOT NULL,
    "UPDATED_BY" character varying(50),
    "UPDATED_ON" timestamp with time zone,
    "USER_TYPE" character varying(50),
    "USERIP" character varying(50),
    "TRN_TYPE" character varying(1),
    "XML_Status" character varying(1),
    "RECOVERY_AMT" numeric(10,2),
    "RECOVERY_DATE" timestamp with time zone,
    "RECOVERY_STATUS" bigint,
    "GOI_QTY" numeric(10,2),
    "SP_QTY" numeric(10,2),
    "VARIETY_AGE" bigint,
    "PREBOOKING_AMT" numeric(10,2),
    "PREBOOKING_APPLICATIONID" character varying(50)
);
 "   DROP TABLE public."STOCK_FARMER";
       public         heap    postgres    false            �            1259    41752    Stock_Agencies_Master    TABLE     �   CREATE TABLE public."Stock_Agencies_Master" (
    "AgenciesID" character varying(2) NOT NULL,
    "AgenciesName" character varying(50),
    "IsActive" character varying(1),
    "Priority" integer
);
 +   DROP TABLE public."Stock_Agencies_Master";
       public         heap    postgres    false            �            1259    33514    Stock_District    TABLE     �   CREATE TABLE public."Stock_District" (
    "Dist_Code" character varying(20) NOT NULL,
    "Dist_Name" character varying(50),
    "NDist_Code" character varying(3),
    "LGDistrict" integer
);
 $   DROP TABLE public."Stock_District";
       public         heap    postgres    false            �            1259    33519    Stock_Godown_Master    TABLE     #  CREATE TABLE public."Stock_Godown_Master" (
    "Dist_Code" character varying(10),
    "Godown_ID" character varying(4) NOT NULL,
    "Godown_Name" character varying(100),
    "User_Type" character varying(4),
    "IsActive" character varying(1),
    "PrebookingGodown" character varying
);
 )   DROP TABLE public."Stock_Godown_Master";
       public         heap    postgres    false            �            1259    33544    Stock_Pricelist    TABLE     m  CREATE TABLE public."Stock_Pricelist" (
    slno integer NOT NULL,
    "Crop_class" character varying(50) NOT NULL,
    "RECEIVE_UNITCD" character varying(4) NOT NULL,
    "Crop_Vcode" character varying(4) NOT NULL,
    "Crop_Code" character varying(4) NOT NULL,
    seasons character varying(50) NOT NULL,
    "All_in_cost_Price" numeric(10,2) DEFAULT 0,
    "SCHEME_CODE_GOI" character varying(50),
    "SCHEME_CODE_GOI1" character varying(50),
    "SCHEME_CODE_GOI2" character varying(50),
    "GOI_Subsidy" numeric(10,2) DEFAULT 0 NOT NULL,
    "SCHEME_CODE_SP" character varying(50),
    "STATEPLAN_Subsidy" numeric(10,2) DEFAULT 0,
    "TOT_SUBSIDY" numeric(10,2),
    "FARMER_PRICE" numeric(10,2),
    "Date_of_entry" timestamp with time zone,
    "F_Year" character varying(10) NOT NULL,
    "VARIETY_AFTER_10YEAR" bigint DEFAULT 0,
    "IS_ACTIVE" bigint DEFAULT 1
);
 %   DROP TABLE public."Stock_Pricelist";
       public         heap    postgres    false            �            1259    33554    Stock_ReceiveDealer    TABLE     �  CREATE TABLE public."Stock_ReceiveDealer" (
    "RCV_NO" integer NOT NULL,
    "LIC_NO" character varying(50) NOT NULL,
    "RECEIVE_DATE" timestamp with time zone,
    "DD_NUMBER" character varying(50),
    "CASH_MEMO_NO" character varying(50) NOT NULL,
    "GODOWN_ID" character varying(50),
    "RECEIVE_UNITCD" character varying,
    "CROP_VARIETY_CODE" character varying(4),
    "CROP_ID" character varying(4),
    "CROP_CLASS" character varying(50),
    "LOT_NO" character varying(50) NOT NULL,
    "BAG_SIZE" character varying(50),
    "NO_OF_BAGS" character varying(50),
    "AMOUNT" numeric(10,2),
    "SEASSION_NAME" character varying(50) NOT NULL,
    "FIN_YR" character varying(7) NOT NULL,
    "USER_TYPE" character varying(4) NOT NULL,
    "STATUS" character varying(1) NOT NULL,
    "ENTRYDATE" timestamp with time zone,
    "USERID" character varying(50),
    "USERIP" character varying(50),
    "PACSRebate" character varying(10)
);
 )   DROP TABLE public."Stock_ReceiveDealer";
       public         heap    postgres    false            �            1259    41757    Stock_ReceiveDetails    TABLE     t  CREATE TABLE public."Stock_ReceiveDetails" (
    "RECVTRANSID]" character varying(50) NOT NULL,
    "Dist_Code" character varying(10),
    "Godown_ID" character varying(4),
    "AgenciesID" character varying(2),
    "Receive_Unitcd" character varying(4),
    "MOU_REFNO" character varying(50),
    "Outagency_Waybillnum_Farmnm" character varying(100),
    "Challan_No" character varying(50),
    "CropCatg_ID" character varying(2),
    "Crop_ID" character varying(4),
    "Crop_Verid" character varying(4) NOT NULL,
    "Class" character varying(50),
    "Lot_No" character varying(50) NOT NULL,
    "Bag_Size_In_kg" character varying(10),
    "Recv_No_Of_Bags" integer,
    "Recv_Date" timestamp with time zone,
    "Recv_Quantity]" numeric(10,2),
    "SEASSION_NAME" character varying(10),
    "FIN_YR" character varying(7),
    "User_Type" character varying(4) NOT NULL,
    "EntryDate" timestamp with time zone,
    "UserID" character varying(50),
    "UserIP" character varying(50),
    "TESTING_DATE" timestamp with time zone,
    "EXPIRY_DATE" timestamp with time zone,
    "FARMER_ID" character varying(50),
    "STATUS" integer
);
 *   DROP TABLE public."Stock_ReceiveDetails";
       public         heap    postgres    false            �            1259    33526    Stock_Receive_Unit_Master    TABLE     G  CREATE TABLE public."Stock_Receive_Unit_Master" (
    "AgenciesID" character varying(2),
    "Receive_Unitcd" character varying(4) NOT NULL,
    "Receive_Unitname" character varying(50),
    "IS_OAIC" bigint DEFAULT 1,
    "IS_OSSC" bigint DEFAULT 1,
    "IS_ACTIVE" bigint DEFAULT 1,
    "SHORT_NAME" character varying(50)
);
 /   DROP TABLE public."Stock_Receive_Unit_Master";
       public         heap    postgres    false            �            1259    41764    Stock_SaleDetails    TABLE     &  CREATE TABLE public."Stock_SaleDetails" (
    "SALETRANSID" character varying(50) NOT NULL,
    "SUPPLY_TYPE" character varying(50),
    "CREDIT_BILL_NO" character varying(50),
    "mDATE" timestamp with time zone,
    "DEPT_TYPE" character varying(50),
    "GODOWN_ID" character varying(4),
    "SALE_DATE" timestamp with time zone,
    "SALE_TO" character varying(50),
    "DD_NUMBER" character varying(50),
    "DD_AMOUNT" numeric(10,2),
    "CASH_MEMO_NO" character varying(50),
    "PRICE_QTL" numeric(10,2),
    "AMOUNT" numeric(18,2),
    "CROPCATG_ID" character varying(2),
    "CROP_ID" character varying(4),
    "CROP_VERID" character varying(4),
    "CLASS" character varying(50),
    "Receive_Unitcd" character varying(4),
    "MOU_REFNO" character varying(50),
    "LOT_NUMBER" character varying(50),
    "BAG_SIZE_KG]" character varying(50),
    "SALE_NO_OF_BAG" integer,
    "CONFIRM_STATUS" bigint,
    "STATUS" character varying(1),
    "SEASONS" character varying(50),
    "F_YEAR" character varying(7),
    "UPDATED_BY" character varying(50),
    "UPDATED_ON" timestamp with time zone,
    "USER_TYPE" character varying(4) NOT NULL,
    "USERIP" character varying(50),
    "IS_ACTIVE" character varying(1),
    "PREBOOKING_AMT" numeric(10,2),
    "PREBOOKING_APPLICATIONID" character varying(50)
);
 '   DROP TABLE public."Stock_SaleDetails";
       public         heap    postgres    false            �            1259    33474    Stock_StockDetails    TABLE       CREATE TABLE public."Stock_StockDetails" (
    "Stock_ID" integer NOT NULL,
    "Dist_Code" character varying(50),
    "Godown_ID" character varying(4),
    "CropCatg_ID" character varying(4),
    "Crop_ID" character varying(4),
    "Crop_Verid" character varying(4) NOT NULL,
    "Class" character varying(50),
    "Receive_Unitcd" character varying(4),
    "MOU_REFNO" character varying(10),
    "Lot_No" character varying(50) NOT NULL,
    "Bag_Size_In_kg" character varying(10),
    "Recv_No_Of_Bags" integer,
    "AVL_NO_OF_BAGS" integer,
    "Stock_Date" timestamp with time zone,
    "Stock_Quantity" numeric(10,2),
    "Avl_Quantity" numeric(10,2),
    "SEASSION_NAME" character varying(1),
    "FIN_YR" character varying(7),
    "User_Type" character varying(4) NOT NULL,
    "EntryDate" timestamp with time zone,
    "UserID" character varying(50),
    "UserIP" character varying(50),
    "Entry_Status" character varying(1),
    "TESTING_DATE" timestamp with time zone,
    "EXPIRY_DATE" timestamp with time zone,
    "VALIDITY" boolean
);
 (   DROP TABLE public."Stock_StockDetails";
       public         heap    postgres    false            �            1259    33489    mCrop    TABLE     �   CREATE TABLE public."mCrop" (
    "Crop_Code" character varying(4) NOT NULL,
    "Category_Code" character varying(4),
    "Crop_Name" character varying(100),
    "IS_ACTIVE" bigint DEFAULT 1
);
    DROP TABLE public."mCrop";
       public         heap    postgres    false            �            1259    33479    mCropCategory    TABLE     �   CREATE TABLE public."mCropCategory" (
    "Category_Code" character varying(2) NOT NULL,
    "Category_Name" character varying(1000),
    "IS_ACTIVE" boolean
);
 #   DROP TABLE public."mCropCategory";
       public         heap    postgres    false            �            1259    33495    mCropVariety    TABLE     �   CREATE TABLE public."mCropVariety" (
    "Variety_Code" character varying(5) NOT NULL,
    "Crop_Code" character varying(4) NOT NULL,
    "Variety_Name" character varying(100),
    "IS_ACTIVE" bigint DEFAULT 1,
    "STATUS" bigint DEFAULT 1
);
 "   DROP TABLE public."mCropVariety";
       public         heap    postgres    false            �            1259    33502    mFINYR    TABLE     o   CREATE TABLE public."mFINYR" (
    "FIN_YR" character varying(7) NOT NULL,
    "IS_ACTIVE" bigint DEFAULT 1
);
    DROP TABLE public."mFINYR";
       public         heap    postgres    false            �            1259    41771    mLATESTNEWS    TABLE     �   CREATE TABLE public."mLATESTNEWS" (
    "NEWS_ID" integer NOT NULL,
    "NEWS" character varying(5000),
    "UPDATED_ON]" timestamp with time zone,
    "IS_ACTIVE" bigint
);
 !   DROP TABLE public."mLATESTNEWS";
       public         heap    postgres    false            �            1259    41778    mMAX_SUBSIDY    TABLE     U  CREATE TABLE public."mMAX_SUBSIDY" (
    "CROP_CODE" character varying(4) NOT NULL,
    "MAX_SUBSIDY" numeric(10,2),
    "FIN_YEAR" character varying(7) NOT NULL,
    "SEASON" character varying(1) NOT NULL,
    "UPDATED_ON" timestamp with time zone,
    "IS_ACTIVE" bigint,
    "GOI_SUBSIDY" numeric(10,2),
    "SP_SUBSIDY" numeric(10,2)
);
 "   DROP TABLE public."mMAX_SUBSIDY";
       public         heap    postgres    false            �            1259    41783    mPACS_DISCOUNT    TABLE     y  CREATE TABLE public."mPACS_DISCOUNT" (
    "CROP_CODE" character varying(4) NOT NULL,
    "SEASSION" character varying(1) NOT NULL,
    "FIN_YR" character varying(10) NOT NULL,
    "TRAN_CHARGE" numeric(10,2),
    "OSSC_DEALER" numeric(10,2),
    "OSSC_PACS" numeric(10,2),
    "IS_ACTIVE" bigint,
    "LOADING_UNLOADING" numeric(10,2),
    "DEALER_COMISSION" numeric(10,2)
);
 $   DROP TABLE public."mPACS_DISCOUNT";
       public         heap    postgres    false            �            1259    41788    mSCHEME    TABLE     �   CREATE TABLE public."mSCHEME" (
    "SCHEME_CODE" character varying(50) NOT NULL,
    "SCHEME_NAME" character varying(50),
    "MAP_CODE" integer,
    "SCHEME_CODE1" character varying(50)
);
    DROP TABLE public."mSCHEME";
       public         heap    postgres    false            �            1259    33508 	   mSEASSION    TABLE     !  CREATE TABLE public."mSEASSION" (
    "SHORT_NAME" character varying(1),
    "SEASSION_NAME" character varying(50) NOT NULL,
    "FIN_YR" character varying(7) NOT NULL,
    "IS_ACTIVE" bigint DEFAULT 1,
    "START_DATE" timestamp with time zone,
    "END_DATE" timestamp with time zone
);
    DROP TABLE public."mSEASSION";
       public         heap    postgres    false            �            1259    41797    prebookinglist    TABLE     �  CREATE TABLE public.prebookinglist (
    "bookingID" character varying(50) NOT NULL,
    "applicationID" character varying(50) NOT NULL,
    "bookingType" character varying(50),
    "distID" character varying(50),
    "blockID" character varying(50),
    "gpID" character varying(50),
    "beneficiaryType" character varying(50),
    "beneficiaryId" character varying(50),
    fin_year character varying(50),
    "Season" character varying(50),
    "dealerId" character varying(50),
    "cropCode" character varying(50),
    "varietyCode" character varying(50),
    "monthOfPurchase" character varying(50),
    "bagSize" character varying(50),
    "noOfBag" character varying(50),
    quantity character varying(50),
    "totalCost" character varying(50),
    "preBookingAmt" character varying(50),
    "totalPaidAmt" character varying(50),
    "dateofPurchase" timestamp with time zone,
    "dateofInsert" timestamp with time zone,
    "IS_ACTIVE" bigint,
    "TRANSACTION_ID" character varying(50),
    dateofcancel timestamp with time zone,
    canceledby timestamp with time zone,
    remarkbydeler character varying(5000000),
    cancelstatus character varying(50),
    "noofBagSale" integer,
    "saleAmount" numeric(10,2)
);
 "   DROP TABLE public.prebookinglist;
       public         heap    postgres    false            �          0    41730    ERROR_CATCH 
   TABLE DATA           �   COPY public."ERROR_CATCH" ("ERROR_NUMBER", "ERROR_SEVERITY", "ERROR_STATE", "ERROR_PROCEDURE", "ERROR_LINE", "ERROR_MESSAGE]", "UPDATED_ON", "USER_IP", "UPDATED_BY") FROM stdin;
    public          postgres    false    225   ��       �          0    41735    STOCK_BAGSIZE 
   TABLE DATA           N   COPY public."STOCK_BAGSIZE" ("BAG_SIZE", "BAG_NAME", "IS_ACTIVE") FROM stdin;
    public          postgres    false    226   ��       �          0    41740    STOCK_DEALERSTOCK 
   TABLE DATA           �  COPY public."STOCK_DEALERSTOCK" ("STOCK_ID", "LICENCE_NO", "CLASS", "RECEIVE_UNITCD", "MOU_REFNO", "CROPCATG_ID", "CROP_VERID", "CROP_ID", "SEASSION", "FIN_YR", "LOT_NO", "BAG_SIZE_IN_KG", "RECV_NO_OF_BAGS", "AVL_NO_OF_BAGS", "PRICE_QTL", "SUBSIDY_QTL", "STOCK_DATE", "STOCK_QUANTITY", "AVL_QUANTITY", "USER_TYPE", "ENTRYDATE", "USERID", "USERIP", "ENTRY_STATUS", "TESTING_DATE", "EXPIRY_DATE", "VALIDITY") FROM stdin;
    public          postgres    false    227   ܊       �          0    41745    STOCK_FARMER 
   TABLE DATA           I  COPY public."STOCK_FARMER" ("FARMER_ID", "TRANSACTION_ID", "CROPCATG_ID", "CROP_ID", "CROP_VERID", "CROP_CLASS", "Receive_Unitcd", "LOT_NUMBER", "BAG_SIZE_KG", "NO_OF_BAGS", "TOT_QTL", "ADMISSIBLE_SUBSIDY", "PRICE_QTL", "ALL_IN_COST_AMOUNT", "SCHEME_CODE_GOI", "TOT_SUB_AMOUNT_GOI", "SCHEME_CODE_SP", "TOT_SUB_AMOUNT_SP", "SUBSIDY_AMOUNT", "SEASON", "FIN_YEAR", "UPDATED_BY", "UPDATED_ON", "USER_TYPE", "USERIP", "TRN_TYPE", "XML_Status", "RECOVERY_AMT", "RECOVERY_DATE", "RECOVERY_STATUS", "GOI_QTY", "SP_QTY", "VARIETY_AGE", "PREBOOKING_AMT", "PREBOOKING_APPLICATIONID") FROM stdin;
    public          postgres    false    228   �       �          0    41752    Stock_Agencies_Master 
   TABLE DATA           g   COPY public."Stock_Agencies_Master" ("AgenciesID", "AgenciesName", "IsActive", "Priority") FROM stdin;
    public          postgres    false    229   ��       �          0    33514    Stock_District 
   TABLE DATA           `   COPY public."Stock_District" ("Dist_Code", "Dist_Name", "NDist_Code", "LGDistrict") FROM stdin;
    public          postgres    false    220   ��       �          0    33519    Stock_Godown_Master 
   TABLE DATA           �   COPY public."Stock_Godown_Master" ("Dist_Code", "Godown_ID", "Godown_Name", "User_Type", "IsActive", "PrebookingGodown") FROM stdin;
    public          postgres    false    221    �       �          0    33544    Stock_Pricelist 
   TABLE DATA           Z  COPY public."Stock_Pricelist" (slno, "Crop_class", "RECEIVE_UNITCD", "Crop_Vcode", "Crop_Code", seasons, "All_in_cost_Price", "SCHEME_CODE_GOI", "SCHEME_CODE_GOI1", "SCHEME_CODE_GOI2", "GOI_Subsidy", "SCHEME_CODE_SP", "STATEPLAN_Subsidy", "TOT_SUBSIDY", "FARMER_PRICE", "Date_of_entry", "F_Year", "VARIETY_AFTER_10YEAR", "IS_ACTIVE") FROM stdin;
    public          postgres    false    223   =�       �          0    33554    Stock_ReceiveDealer 
   TABLE DATA           L  COPY public."Stock_ReceiveDealer" ("RCV_NO", "LIC_NO", "RECEIVE_DATE", "DD_NUMBER", "CASH_MEMO_NO", "GODOWN_ID", "RECEIVE_UNITCD", "CROP_VARIETY_CODE", "CROP_ID", "CROP_CLASS", "LOT_NO", "BAG_SIZE", "NO_OF_BAGS", "AMOUNT", "SEASSION_NAME", "FIN_YR", "USER_TYPE", "STATUS", "ENTRYDATE", "USERID", "USERIP", "PACSRebate") FROM stdin;
    public          postgres    false    224   Z�       �          0    41757    Stock_ReceiveDetails 
   TABLE DATA           �  COPY public."Stock_ReceiveDetails" ("RECVTRANSID]", "Dist_Code", "Godown_ID", "AgenciesID", "Receive_Unitcd", "MOU_REFNO", "Outagency_Waybillnum_Farmnm", "Challan_No", "CropCatg_ID", "Crop_ID", "Crop_Verid", "Class", "Lot_No", "Bag_Size_In_kg", "Recv_No_Of_Bags", "Recv_Date", "Recv_Quantity]", "SEASSION_NAME", "FIN_YR", "User_Type", "EntryDate", "UserID", "UserIP", "TESTING_DATE", "EXPIRY_DATE", "FARMER_ID", "STATUS") FROM stdin;
    public          postgres    false    230   w�       �          0    33526    Stock_Receive_Unit_Master 
   TABLE DATA           �   COPY public."Stock_Receive_Unit_Master" ("AgenciesID", "Receive_Unitcd", "Receive_Unitname", "IS_OAIC", "IS_OSSC", "IS_ACTIVE", "SHORT_NAME") FROM stdin;
    public          postgres    false    222   ��       �          0    41764    Stock_SaleDetails 
   TABLE DATA           �  COPY public."Stock_SaleDetails" ("SALETRANSID", "SUPPLY_TYPE", "CREDIT_BILL_NO", "mDATE", "DEPT_TYPE", "GODOWN_ID", "SALE_DATE", "SALE_TO", "DD_NUMBER", "DD_AMOUNT", "CASH_MEMO_NO", "PRICE_QTL", "AMOUNT", "CROPCATG_ID", "CROP_ID", "CROP_VERID", "CLASS", "Receive_Unitcd", "MOU_REFNO", "LOT_NUMBER", "BAG_SIZE_KG]", "SALE_NO_OF_BAG", "CONFIRM_STATUS", "STATUS", "SEASONS", "F_YEAR", "UPDATED_BY", "UPDATED_ON", "USER_TYPE", "USERIP", "IS_ACTIVE", "PREBOOKING_AMT", "PREBOOKING_APPLICATIONID") FROM stdin;
    public          postgres    false    231   ��       �          0    33474    Stock_StockDetails 
   TABLE DATA           �  COPY public."Stock_StockDetails" ("Stock_ID", "Dist_Code", "Godown_ID", "CropCatg_ID", "Crop_ID", "Crop_Verid", "Class", "Receive_Unitcd", "MOU_REFNO", "Lot_No", "Bag_Size_In_kg", "Recv_No_Of_Bags", "AVL_NO_OF_BAGS", "Stock_Date", "Stock_Quantity", "Avl_Quantity", "SEASSION_NAME", "FIN_YR", "User_Type", "EntryDate", "UserID", "UserIP", "Entry_Status", "TESTING_DATE", "EXPIRY_DATE", "VALIDITY") FROM stdin;
    public          postgres    false    214   ��       �          0    33489    mCrop 
   TABLE DATA           Y   COPY public."mCrop" ("Crop_Code", "Category_Code", "Crop_Name", "IS_ACTIVE") FROM stdin;
    public          postgres    false    216   ��       �          0    33479    mCropCategory 
   TABLE DATA           X   COPY public."mCropCategory" ("Category_Code", "Category_Name", "IS_ACTIVE") FROM stdin;
    public          postgres    false    215   ��       �          0    33495    mCropVariety 
   TABLE DATA           l   COPY public."mCropVariety" ("Variety_Code", "Crop_Code", "Variety_Name", "IS_ACTIVE", "STATUS") FROM stdin;
    public          postgres    false    217   ��       �          0    33502    mFINYR 
   TABLE DATA           9   COPY public."mFINYR" ("FIN_YR", "IS_ACTIVE") FROM stdin;
    public          postgres    false    218   W�       �          0    41771    mLATESTNEWS 
   TABLE DATA           V   COPY public."mLATESTNEWS" ("NEWS_ID", "NEWS", "UPDATED_ON]", "IS_ACTIVE") FROM stdin;
    public          postgres    false    232   ��       �          0    41778    mMAX_SUBSIDY 
   TABLE DATA           �   COPY public."mMAX_SUBSIDY" ("CROP_CODE", "MAX_SUBSIDY", "FIN_YEAR", "SEASON", "UPDATED_ON", "IS_ACTIVE", "GOI_SUBSIDY", "SP_SUBSIDY") FROM stdin;
    public          postgres    false    233   ��       �          0    41783    mPACS_DISCOUNT 
   TABLE DATA           �   COPY public."mPACS_DISCOUNT" ("CROP_CODE", "SEASSION", "FIN_YR", "TRAN_CHARGE", "OSSC_DEALER", "OSSC_PACS", "IS_ACTIVE", "LOADING_UNLOADING", "DEALER_COMISSION") FROM stdin;
    public          postgres    false    234   ��       �          0    41788    mSCHEME 
   TABLE DATA           ]   COPY public."mSCHEME" ("SCHEME_CODE", "SCHEME_NAME", "MAP_CODE", "SCHEME_CODE1") FROM stdin;
    public          postgres    false    235   ��       �          0    33508 	   mSEASSION 
   TABLE DATA           u   COPY public."mSEASSION" ("SHORT_NAME", "SEASSION_NAME", "FIN_YR", "IS_ACTIVE", "START_DATE", "END_DATE") FROM stdin;
    public          postgres    false    219   �       �          0    41797    prebookinglist 
   TABLE DATA           �  COPY public.prebookinglist ("bookingID", "applicationID", "bookingType", "distID", "blockID", "gpID", "beneficiaryType", "beneficiaryId", fin_year, "Season", "dealerId", "cropCode", "varietyCode", "monthOfPurchase", "bagSize", "noOfBag", quantity, "totalCost", "preBookingAmt", "totalPaidAmt", "dateofPurchase", "dateofInsert", "IS_ACTIVE", "TRANSACTION_ID", dateofcancel, canceledby, remarkbydeler, cancelstatus, "noofBagSale", "saleAmount") FROM stdin;
    public          postgres    false    236   �       �           2606    41739     STOCK_BAGSIZE STOCK_BAGSIZE_pkey 
   CONSTRAINT     j   ALTER TABLE ONLY public."STOCK_BAGSIZE"
    ADD CONSTRAINT "STOCK_BAGSIZE_pkey" PRIMARY KEY ("BAG_SIZE");
 N   ALTER TABLE ONLY public."STOCK_BAGSIZE" DROP CONSTRAINT "STOCK_BAGSIZE_pkey";
       public            postgres    false    226            �           2606    41744 (   STOCK_DEALERSTOCK STOCK_DEALERSTOCK_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."STOCK_DEALERSTOCK"
    ADD CONSTRAINT "STOCK_DEALERSTOCK_pkey" PRIMARY KEY ("LICENCE_NO", "CLASS", "RECEIVE_UNITCD", "CROP_VERID", "CROP_ID", "SEASSION", "FIN_YR", "LOT_NO", "BAG_SIZE_IN_KG", "PRICE_QTL");
 V   ALTER TABLE ONLY public."STOCK_DEALERSTOCK" DROP CONSTRAINT "STOCK_DEALERSTOCK_pkey";
       public            postgres    false    227    227    227    227    227    227    227    227    227    227            �           2606    41751 (   STOCK_FARMER STOCK_FARMER_2021-22_R_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."STOCK_FARMER"
    ADD CONSTRAINT "STOCK_FARMER_2021-22_R_pkey" PRIMARY KEY ("TRANSACTION_ID", "LOT_NUMBER");
 V   ALTER TABLE ONLY public."STOCK_FARMER" DROP CONSTRAINT "STOCK_FARMER_2021-22_R_pkey";
       public            postgres    false    228    228            �           2606    41756 0   Stock_Agencies_Master Stock_Agencies_Master_pkey 
   CONSTRAINT     |   ALTER TABLE ONLY public."Stock_Agencies_Master"
    ADD CONSTRAINT "Stock_Agencies_Master_pkey" PRIMARY KEY ("AgenciesID");
 ^   ALTER TABLE ONLY public."Stock_Agencies_Master" DROP CONSTRAINT "Stock_Agencies_Master_pkey";
       public            postgres    false    229            �           2606    33518 "   Stock_District Stock_District_pkey 
   CONSTRAINT     m   ALTER TABLE ONLY public."Stock_District"
    ADD CONSTRAINT "Stock_District_pkey" PRIMARY KEY ("Dist_Code");
 P   ALTER TABLE ONLY public."Stock_District" DROP CONSTRAINT "Stock_District_pkey";
       public            postgres    false    220            �           2606    33525 ,   Stock_Godown_Master Stock_Godown_Master_pkey 
   CONSTRAINT     w   ALTER TABLE ONLY public."Stock_Godown_Master"
    ADD CONSTRAINT "Stock_Godown_Master_pkey" PRIMARY KEY ("Godown_ID");
 Z   ALTER TABLE ONLY public."Stock_Godown_Master" DROP CONSTRAINT "Stock_Godown_Master_pkey";
       public            postgres    false    221            �           2606    33553 $   Stock_Pricelist Stock_Pricelist_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Stock_Pricelist"
    ADD CONSTRAINT "Stock_Pricelist_pkey" PRIMARY KEY ("Crop_class", "RECEIVE_UNITCD", "Crop_Vcode", "Crop_Code", seasons, "F_Year");
 R   ALTER TABLE ONLY public."Stock_Pricelist" DROP CONSTRAINT "Stock_Pricelist_pkey";
       public            postgres    false    223    223    223    223    223    223            �           2606    33560 ,   Stock_ReceiveDealer Stock_ReceiveDealer_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Stock_ReceiveDealer"
    ADD CONSTRAINT "Stock_ReceiveDealer_pkey" PRIMARY KEY ("RCV_NO", "LIC_NO", "CASH_MEMO_NO", "LOT_NO", "SEASSION_NAME", "FIN_YR", "USER_TYPE", "STATUS");
 Z   ALTER TABLE ONLY public."Stock_ReceiveDealer" DROP CONSTRAINT "Stock_ReceiveDealer_pkey";
       public            postgres    false    224    224    224    224    224    224    224    224            �           2606    41763 .   Stock_ReceiveDetails Stock_ReceiveDetails_pkey 
   CONSTRAINT     |   ALTER TABLE ONLY public."Stock_ReceiveDetails"
    ADD CONSTRAINT "Stock_ReceiveDetails_pkey" PRIMARY KEY ("RECVTRANSID]");
 \   ALTER TABLE ONLY public."Stock_ReceiveDetails" DROP CONSTRAINT "Stock_ReceiveDetails_pkey";
       public            postgres    false    230            �           2606    33533 8   Stock_Receive_Unit_Master Stock_Receive_Unit_Master_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Stock_Receive_Unit_Master"
    ADD CONSTRAINT "Stock_Receive_Unit_Master_pkey" PRIMARY KEY ("Receive_Unitcd");
 f   ALTER TABLE ONLY public."Stock_Receive_Unit_Master" DROP CONSTRAINT "Stock_Receive_Unit_Master_pkey";
       public            postgres    false    222            �           2606    41770 (   Stock_SaleDetails Stock_SaleDetails_pkey 
   CONSTRAINT     u   ALTER TABLE ONLY public."Stock_SaleDetails"
    ADD CONSTRAINT "Stock_SaleDetails_pkey" PRIMARY KEY ("SALETRANSID");
 V   ALTER TABLE ONLY public."Stock_SaleDetails" DROP CONSTRAINT "Stock_SaleDetails_pkey";
       public            postgres    false    231            �           2606    33478 *   Stock_StockDetails Stock_StockDetails_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Stock_StockDetails"
    ADD CONSTRAINT "Stock_StockDetails_pkey" PRIMARY KEY ("Stock_ID", "Crop_Verid", "Lot_No", "User_Type");
 X   ALTER TABLE ONLY public."Stock_StockDetails" DROP CONSTRAINT "Stock_StockDetails_pkey";
       public            postgres    false    214    214    214    214            �           2606    33485     mCropCategory mCropCategory_pkey 
   CONSTRAINT     o   ALTER TABLE ONLY public."mCropCategory"
    ADD CONSTRAINT "mCropCategory_pkey" PRIMARY KEY ("Category_Code");
 N   ALTER TABLE ONLY public."mCropCategory" DROP CONSTRAINT "mCropCategory_pkey";
       public            postgres    false    215            �           2606    41795    mCropVariety mCropVariety_pkey 
   CONSTRAINT     y   ALTER TABLE ONLY public."mCropVariety"
    ADD CONSTRAINT "mCropVariety_pkey" PRIMARY KEY ("Variety_Code", "Crop_Code");
 L   ALTER TABLE ONLY public."mCropVariety" DROP CONSTRAINT "mCropVariety_pkey";
       public            postgres    false    217    217            �           2606    33494    mCrop mCrop_pkey 
   CONSTRAINT     [   ALTER TABLE ONLY public."mCrop"
    ADD CONSTRAINT "mCrop_pkey" PRIMARY KEY ("Crop_Code");
 >   ALTER TABLE ONLY public."mCrop" DROP CONSTRAINT "mCrop_pkey";
       public            postgres    false    216            �           2606    33507    mFINYR mFINYR_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public."mFINYR"
    ADD CONSTRAINT "mFINYR_pkey" PRIMARY KEY ("FIN_YR");
 @   ALTER TABLE ONLY public."mFINYR" DROP CONSTRAINT "mFINYR_pkey";
       public            postgres    false    218            �           2606    41777    mLATESTNEWS mLATESTNEWS_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public."mLATESTNEWS"
    ADD CONSTRAINT "mLATESTNEWS_pkey" PRIMARY KEY ("NEWS_ID");
 J   ALTER TABLE ONLY public."mLATESTNEWS" DROP CONSTRAINT "mLATESTNEWS_pkey";
       public            postgres    false    232            �           2606    41782    mMAX_SUBSIDY mMAX_SUBSIDY_pkey 
   CONSTRAINT        ALTER TABLE ONLY public."mMAX_SUBSIDY"
    ADD CONSTRAINT "mMAX_SUBSIDY_pkey" PRIMARY KEY ("CROP_CODE", "FIN_YEAR", "SEASON");
 L   ALTER TABLE ONLY public."mMAX_SUBSIDY" DROP CONSTRAINT "mMAX_SUBSIDY_pkey";
       public            postgres    false    233    233    233            �           2606    41787 "   mPACS_DISCOUNT mPACS_DISCOUNT_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."mPACS_DISCOUNT"
    ADD CONSTRAINT "mPACS_DISCOUNT_pkey" PRIMARY KEY ("CROP_CODE", "SEASSION", "FIN_YR");
 P   ALTER TABLE ONLY public."mPACS_DISCOUNT" DROP CONSTRAINT "mPACS_DISCOUNT_pkey";
       public            postgres    false    234    234    234            �           2606    41792    mSCHEME mSCHEME_pkey 
   CONSTRAINT     a   ALTER TABLE ONLY public."mSCHEME"
    ADD CONSTRAINT "mSCHEME_pkey" PRIMARY KEY ("SCHEME_CODE");
 B   ALTER TABLE ONLY public."mSCHEME" DROP CONSTRAINT "mSCHEME_pkey";
       public            postgres    false    235            �           2606    33513    mSEASSION mSEASSION_pkey 
   CONSTRAINT     q   ALTER TABLE ONLY public."mSEASSION"
    ADD CONSTRAINT "mSEASSION_pkey" PRIMARY KEY ("SEASSION_NAME", "FIN_YR");
 F   ALTER TABLE ONLY public."mSEASSION" DROP CONSTRAINT "mSEASSION_pkey";
       public            postgres    false    219    219            �           2606    41803 "   prebookinglist prebookinglist_pkey 
   CONSTRAINT     z   ALTER TABLE ONLY public.prebookinglist
    ADD CONSTRAINT prebookinglist_pkey PRIMARY KEY ("bookingID", "applicationID");
 L   ALTER TABLE ONLY public.prebookinglist DROP CONSTRAINT prebookinglist_pkey;
       public            postgres    false    236    236            �      x������ � �      �      x������ � �      �      x��}ˮ$�q�z���aL+�^Y�$@�!&��6�,,Ȑ��wfUwUVu\�:����|���Ȉȸ�K�?~�����W�g���˦?3Ɣ������������X���ߵ����Ʀ�5���O?�q��?��~��/[ڧ�߿Jm�����޺�1?������?1��;~Z�4���ù�`�ӛ�#�����Kc������������������������!>��h6���� ����1����_��	������uF������3������,H�����$��Ͽy�5|TNBj�I�)r	ٰ%��l˗q�lҮV��������3�v�Gί��?��������?lm|���u����������.$%���@��,wk\���Ӻ��_2}m`0Ɖ���f�qҎT`1q��4N|������~��N�*'������~ʧO��ҝHA����n��%����$%��&FLj^(��#�������$(.`�|��KêXi_�a�s��`�gc%T���_�O�ʤ�4
�+Y5)���/���x&a����n��I1���1BR��<@J�{k�JY%%����?���?mz����c����Ӝ����ךJ�n�	��_�����K�����~��4��"�T߿ѢQ,�W 4�K���n�R�	�h9�J5�۶|PJ�Ϧ9m�=��V��3tH�dӔT�� c��fRhI	��Q�3�G��ath�vq��N̎]4��ˇ�5�A9�D�'}�,�5zS�D�:W8quy�Uͭ	�"�3;Tsc �~�1�[5�F�9���r��3���[1 �?<�_k�h��_[�����ƃ����C2����ź��������1��Ŷߥ<�k��fV�G�b��	y��Kwb�7��A���\���n�����r���+0i\	%��	���i����i�1�nx��Ǒ̽w�R�5�"(WH�f�D %�O�n
��
�U�4K�2=,P�����r|@�yޚ��^�c�1q�`�aӾ�v>�)�M�.����9ù�p�Ή/юlS�s.?�S༓���i�3���W�))�����[�8�aD���C��!��F:�b��5|�����g��� �XΈ���
F�ଓ����"����HP0⚏������qf%tP�D�tJ����,���g�l�AQ��50="`>�[0Xqgzg>��E��G�fg:�Ht�Y���������t��.�Ȥ������q�L�i��4��)��7�H��s	�Po�~��M���}���iJ$�Jd�����t���*�����b>�V���w��`�X�@Z4�iЅ����xfL{���jW��?n���ln c|���r�?f�f`m\�!y��=�&/I�Ya�yqVv��\�+H�����X|�͋��EI�����d�7 �_�g�	��U�8�SYO@O��8���N�m��\���3�����o�t�?�"�<����C,'��6�w<�#SVC�6��\-_��U�F�uzYP����#��9��C�IIUP"x&��I�y����7'+8��������ބ$�7AX�y�7�`�7Bzyʉ������,V���9^�f�^�X������"�~S,.��\���;@HV�<�@��9s�}�V�6�*I谜/C�I��MN'%iHi�2��)�O�;��:D�\S��3�Q_�8��ﬥ������12�a`.oÍ�Nʥ�X���l����k�"P��N
�!�ln��o(Ųp��1]/|K������� ���zB�r%���4�E�M�?�K�<cz����}[BO�\5@
�-�7/f�YK�sX��0������iן(3;|&3�� f�u_M��A;V�O[�(l}�Y���qlX�t�ڙ=M���|��*aqml���6�P�njJ���|��1��Qɹ��]9��b9M{��S蓋Q�.%�()1�W��\��R�	A��D��7ȯjƸ4ZXY�	�(}��*��o
ʕZ���HLO��"p�� �#���~�� EZmO;�k�Z��Q�����5m�@f�%q��ӭ�ζ��\�$�<�oPr|%(6!Щ�tn�ߢ��H�pa`�_����%�c�P^>~^>Yq[�gQ&3Ah��\��x��H
u���l�ӱ���бW���� <{�Ia��3�ᒤ X�`������+ShN֦��k��K�`M��]%��D@J�ܮ��a�	��ة�*&��%N�*~�`EL�wQ���~�H�>��eK��erL���W	)'�ue�+U�a}����s��G5��{���6��cb�j+�%z[b�z�T|gDр~��--�'���Ǌ�����;�Puɮ~2㮘�z��N�⁅���Q�2
D��`��u=p���߬�������2z/0<k�_�Cp�K?�ӷ�������!&�`�ǦM���������r =�%�(9A�sc��iQ�o��4Ɠ�Ĵi�����KT�"$)mϙ��A v2%VA	[�S�f��!Caڽ`�����)	
J$������ͳAj<SN�}�7�J��]p��@g�1c�M)9��@<�����8��Җg<g������輭��i�\P8�b犉7
F��r!I���'�@vJ���
:~�q��{�];YF��ied;}�E�^����&q�=prc
�*%v��N��>��?��gF��_6Mb[�=V�������!��N��ǙiXň�K~�v��%�eO�� �7;l���/��*��*J�f�Q9l��g9q^�ُ_=�ك�r!Ӄ��$���r�M0x&'�T�?�X;�4�$qb�24���C��'�N=·�S=��C�w$i�E�3K�w�!�������s.(��٧k:�������)�ѹAJ�[䑬R��(SC�\H�\��%J�����`JVÇz���!�(8d ߇���(#��0��zR D�tI[�q�K�|����.�d�R����tJ _�Dpdzi�s����҂�ɤ@�^�z)��r��VE��UkrX�ɚ�UP~`���|y� ��@������7�0/ɨ?���mp{�ӱ��@�lF W/$,>p��9�2���f���Y�o9@���(V$#X�I�3���'wv�@��$?cm��Ñs;�/�x�y���#�~�����L��0fb�F�A�����Hm0Xێi�JJ���	����#���:�N��`!�"�����JBj)wE�-\�K�UW�����BP�n7�^�P��̆z�0��o=b�B�{ x�����pF��ć�֡�.��q��DBG0x��Z�z��%}Z,
��X�L����V@�`��K� �=�q�9ݝ3��vH ���:M� �:�H�7�.#׉a�nuE�I�x�� ������Q�m!fm@���b�('�6�vU��?�f-L�z��Ӭ[BF�X%ɛ�z5f�ѫ�%LR��2���'.����H_���A;��3�a��3����ߦ2��v�u E�ه��� f�%�L<�.��)��c�l�{xzX���ʎ7J��M�6.l���Hk-��?��_�W�k�w�_=�����^��܁�ڷ����E�{��`F>����B��>�f�����ۗi�[��l��ҿ�����q��o�Z���ۚ픾��_ <s쀯)���������5�	�ۏp�; ��2��+w;#^��$����X�4�Iѯ{o�D)O� � ���g��D��i�8%����:�sG�p�L��B��)��(�V�P�$�����*]��8>��IH���j
P��eLK%���3�k��V��HQ0"Q$�O�%���E0!B��5�s=���]�$��(������|�����u��k�R	���pr���о�'�L[��e��$'��ĕ�d� sCp��n�لԂ����W�Qo�^p��Lν���1�MT��v��gi9~�93�`p����Xva|X�i����?|�L�<��)(]Ϧ/Q�~�UA�u�    ��z),�
��6�!���cC���G�¿u���AQ�5!���F�BE0�?e�q�=A������ �'E0�A��{<�XL<���Kr��Ρ��%�K��6�AQJFC�(��/�B�3�5.�[���mq�O�I('ᱏ��{��D�-��GW��n�a�4��n�8h�2�vN`��y������ubcj�y��!�FI���2q;u^cc$��5��:k���)]ڴ+���Ζ��MF�$�7��ҋ�1���fgRҤ�*����Tq����S]��vm����;j�0���pz�!2�Z0x2#NÈ�7X2�b=&5A�s;�z�cD<�B*$�*�$�i�/R�H�Q��)ɩ�v�(�Ø,��(
r���*Tͽ�� ߕ��!q�"��X�g�k�}�򰔔�й���|��M����d���	`n.-�9�]`���snS��q�G�ܡ�I��@a��������BbU�W��)q����3�)�ɹ����ԉ�80x���"�z��{\	9�b����W\'jO:��6lH���ı��z<%�,�������W��+���|ة��9@nɝa���\��+#�JnW�˴H~� j(8�'K	d�1J���B")��#���琦�r,����^|��h����h�{�Q!w�"XyW҅E-��C�)P�8ܔ�����������o:�{��(����Ci�e˚���e*^P��|�U���&*�K��,O69�_�ݓ��";����(�A�ͺʐ�h���}�v�L��3
�.H��K���<+B?����2��z��O�L+ܬ�U�"ZgH׊	�l�ʍ΅m�Iet���N��K�
s����+T����C�Z�\�g<5L��d���G��X_�C�ތ`����,�<��i찭��8�'�tN�����\�Cznn'�0rAJ�{�1"��SR���خtl�c��ۓ�j��13)A�mP
��AI�+�>��`$DŽ	�m�w�C�b�܎�2[���u���r9agF]S�N�5��}j��s&|x$Ͻ�A0ӦnL��	a衃r���>�gH��YVb��'xfʢ	
dt��R�d������4V�ag�Jsa��Xb��۷OjӜ�a�60��N��U���W'eR�@>�wK���oS9 �U���"�.�b��&�r��"f7��\�@5��@5&#<X�L.bC�SG3��KpF(�~��\s}t����qh�<�ĥ��L�OP�BAʹX-IX�}nk3�6ݮ8+ \5WM��	�F�ɑ�A�Q�i)˳�F�}��+ς�L_�Fnt��ì�oP�	�����Uʵ��h4�G<��<��ž�������t'=���H8���}�$O�8>�3�>
"��wo
�ށ���S~����Xh�9�Rz���DH� �2�'�;���oQ2��F��鈢��'��׎&t"lec����e������~.pո���#4�������<s��c��k_�%�b��d����z���摘�$�	I��9�	�
B(��mm���옢5���R�Z8A'~���8	,%�7�>�s��"��N؉�ao�8���C8�K����}�g+<��n��Â`V���F=�ٛq[�R��3�d5"�{� �\A���Z�7QA�d��������z�N����wG��]ܤ�|���+a\��7�Cb�<����jM����/o�1{��ƃ!ãa0W�v����$�s"Mӫ�Ǭ�3k8�*�g|r��L�R%��}����_���r#�����	$'W�l�GX��t�gbU�h�!Ք!��ڱ"؅j�y����h�-C����=(#فo���.y� �/^lI=��+ЗU4F ���r�_�����6��E�c/���Z�k��>��SF�9b5�-�Az �s;���!a_$�^?�#"�$z���Z�7�rY<s�Ihf [^/��NO�qn1$���	"�J0��)��ȯGI�D���9Ö�A�0XE��������8�$-��v����� ��؂�NI�(��Q"(����z��S7)��������6�@X��\XN�j�.'	��Qrb��`����f����#���S�	#ih�H�(#rS�o<v:#Г�:#K� �?F�� �(!�H�o��u�F�����A?x��B����M�a%��ō=�`?���}����x�6qr���v|��pL�O`���(���a��	���F��L�lx��1��1�Z�AK�D��X�(gy�
�vΣ�.�/���
}�oH�8i�� ةk�+����
~?�z��pY���	��͌Vh���褬����:s�cgP)_DG�y����t�*N���±('�m-�.�#Q)C�Nz�@�D��-'��r����F���y�g����R���YK
+�S�>ޏә0$�`�5�Be88�"G���C|��3Q���27�E(@m���t;����*�/֊i�M��@4]��=J {�('8�/I⎇S2�$;� R�2"�����d������ʇ@�@��<�}8dR]��'�4�&@D�~�%�D	�Zj�4���d	��X��y�IX���1��[)[��A�i�0��Bqiw#ϵ�������Y�6_��~ �kG�\1���(k��+D��gt��� X��h��X(��"�	>�C歝Q�H��>~�൧�ZY���g��@ 0u�U�F;Z�ੇ"����Hyح���<�6��e��x�����^靌y_����CX��f_�o�h� B�R��9]?���\��KPV�ꕉ�͞l���SY1��=�|!�1|��˥]�6봽��X0x�1Nl��@�#��xX���k���4��gO.�C���g�p�P��߫���;�4O���.��r8g>��9l\�}Lɬ0�9�q]��X���;�3>
�4O�x�|q�����,(b��v�(�R"/�ޝ��t�L
���I$� '����r��%�ߢ�[�%�J?�{���,�7g�<�����3�:'@���K���9�ŏ v2'�"���z����i9[���R�ODo;�=���z�t��m_�ก0X�x���L�"'�/u�˳�^l�h�q��UO��m� ^Ίt34j8���ŦF��@�f�6�V���9���T����d�-^�〨����~٩�mH��	�UQ���e��%���
�ta�Q@�n<b'�5�*J��?n��߻m���X<uBn m0ʈpBҡ���P=X��g�5q,�B)��1B{��w��gt�J�,��p7f�t�W�>�*p��/>�'��3����@>ɍ)�A0K��G��v����ɂ`N��;.I���#w_���hZ��'H|Ou_��Q�"�PI�-������n��kʂ�h&jщ���a�V<u��	��W%�vA2cpR�(�<M|D�gC�LV�܊N�S�n0�֎��u��2��>]l_�柄׏㵁�̭��5>�<���;4��}Zc�TL��G`0�J��ߺ�
"��rQFcl9���"%>�$' x�c�Ƥ!E<��%-S^c�p��m0x�HXꡔ��
�6�N��+	6?̷��݃�X��mN.��7#�����1�1kwú�L�Pq�O�2B�oDa;#AÈ �c��Gڿ��M`�=�UѢ��m�$-���ƦO_>=k8�py��I�KIJ��1�����:n�H�޵dMេ���5�Qمe X���'�N�3�SP��s�e��(v.#������^�9������cS!�Ľ�[�֠�5� s�������ބ�`<�	;s�X��87�2Ajg<g�\E,�:U�pc����jO6t�����ߏE��+&�_x+��R�e�k��aR��o�!�X��|��=!��S�+�{��W��X�� X%'�i�9�.O�$�⻒��G r`L1�ekq�=9���u��ճ�=�-'EA�xŬXN�N��H�v�r�P[�i�A	�<~�>�1%<��tR���E�JqV\��"_!    s��4[�eN���i|!a���:�D+̱~�l�(@��+P*�P)a+��w�p�Z�0�<G ���"_�+V��PO��� ��	�E9a��ம��x<ee�=0X�(#�M�j<|��2����A��k����F�ZCc�.zb����D	��j��n�@����ucNx�D��X�B{!���a�[�%�ɇ�VvN��nm�� ʊ�(X:�EP�r��qx�`� x��@F#�2�/1᳁ǃZ.��g�	�D{�� Yq�%�@K���ޒ5�x��PJK����l��ó��QIu��A����+��!��`���'бIX�c{�"�G�<���JU�����E���j��'����j���l��@�.O8���ޗ�{��*D��V�)���{�y����/�n�}�{y���o~�ʷ�_(��L�7Y{#S�H1�@��R	���'��K�ߢ�0��K���i���u���l&��ܳ�N"=�o�2RS��G1́�׃XU�K��I��t�������l��=�:�[pЋ�
@ȍʂX�Fi�^�Mj$+َЙ�I9F�}�������.�ݎ\���OJ�W�W�Hk�����{�0u�`'V�u!�*F���m��2X,%WZ��)_�$A�+���C�ix��}�g��F�vw�=.��Jj@�A��d�`��o�s���,�xy'��#^��,����9Vp���4r"����:"qÂ ��vQo���1�OPQn�O�z�V��R<���8���X��CI�{0V�ag��:%��r}NAb���z����w��-%�:�1� ��z�,������`תX����͏Ӹ�Z�cD�!��a��dd�9�$�8�k����r��'s�!/Vю��b<���\�miE@���a"�����A0�X�>�p���'
#؉��� ���!�b�f.�c�]��`5�(U�+�_*"�@��ۛ>Y�.��0��70��$ߩ �.�ww�\��P�ڷƲ�5��o��&@��`�e�iF���ϐ�1?,�����y
���A������^H��jdD?��hd�g$J�n�ҼX{X�p�Yv�d��4z�[��U�K����B۟w)7�s�w�(疄^�i�#�_�	f�)̭>�&-
V��r�� b��T,E���L�� ��>^MB	)N!!E8��(!\W�780 �$E%I�����G�(�䷆��ɦ%+�7��+���mg�h���>��d)�8w��%�U;'-�o~K�Ю�;QJB�d�%���ac�]no_��G��!�����@X��d������벾@	;�s�g(= T*)%X>�B�A�r�Hg9�&g8%���N��7:����m}<S��BO����c����|�q�	t
�����p�,κ?�j,84n�G`g��J�2+��RN�j ��Х��j8a[���T���y�T�#ة)�FI�*%kf�0ݒ $��0xn�;H��ݠ_�℧�}(�/���3��:HH��C�7��<Dә\�k4_�r�PNno92p��d�zy&(#_5��!��R0���p�&�͹>>XZ[��(�tO�in�Ck�)C�3c-�!"����E���d˓!'["ؙ�T���|m�⨝F�������2
Ak���Dc��s;H~��#�σ�������}c��_��=�R(�w���2.E�>c}��&�~Ì ��ㆄt>
χ:INf��C:χ�5��S��=l[Ȉt]70�s�ȏE�A��}�b߷+"E�g�@�_����qK���-���<y�޷tI�2)kl���H�������+��'�W��_"e���[��V����-=��%0x�n�~y4E`�WW��{w�P\�ŉ�� v�ͱ�:��~!�6�a�Aۿy7�x$��<�[@���
y;�2�!1����8#��˵�D���Γe�x�v�� �+J�|��TN�!Q0x2%��A)�����x�k�N�$((M�a@�#r�!�����F`q:���k_��O#$$D��vkdD���{��HJA�)���5k�o}�_�ծ x�x�N��P"����MN����S�!�TJ�@D�|w>�rJC�	c��-�.��h8���0�ء�OS�~c`�����'��>�oe�®Ǆ�SY�9k�jB9���P��������L?��K*��5)w��HvY�e�ӗG��[X��K�e����1w`˂��#��n�|�)��A��i)�|����Hڕ�{�t'��a�9�j�~s�(ʈ4Z�4�_{b�����횐@v��4�Ϛ5JF��0�Uuo�&���F���ç�yPT���ӹfH���X��|gve57ޢ�;�1�cO\��*IB9�k�.1}�����p�3�I�3)��eR�$�7�L&�@Br� r��!m̑�O]����s/����ٜ�fB�W�s�� ���\��|Z{��������yQ�8z���(�9��O{�A�:��c��A���q$ŕ=����$E�����g5Қu8ev\��H�5�1L��s����`ɲc�ҋ�MR�X�3���V�tJ^É`?u�$���p������X `�5��S�
N$Vx�o?=D��7��;$��d�Ət�"���� �׏՞VVY,V�3�����EnG�Ş�fw�d���=zh��r;wWw�AC����]�ݧظ��۞r���O,�ɝ�ɮ��'�����	����|���f?���XU�^�/�IIT0"�M�W!%��1����Ic%)X�����vL�-���*N�I�bT��gw�?���Cx�Moz4�r@�������H�0¿�?��Dr ����K�"ri�[�H2�߅>'�݅Ľ�A�̸Z�5��{z��j����~i2�vkR[�r�Z!)��}i~�pJ~9�����$8%_����OY ��g��Xc���npR�;1��񤕫��sEp�PZ���:��9l x���l4�����;i���Ho�Mݸ�,��*`�|������#�ύ�&�
۩��7�S���������w�ڟ�zZ5fL���x% V,~�-�e��jTr ���$�U�Z����� xf�E2N�ǅ�F2m��|���ז�H`0��1�9W�Y�bE���~�{��l|<���{� E�=����@�LR���[r'M�g��#��aw@��m�e�!#E�z�_	MWd�gJJ�wN�|�ᷦ��әvHY����v<!���6��_N�.'5��(x�0�� �5���=u���kE�-�fK���?�0̗�+4�|eF����w �� �BR�[j0_�cuS���k&�l�LI�UI���X�H��u�%�����]0x�C��M�2+k��1oN\��9�ؼ�.�Up"\�E���!D��!g���U�yo )Iw�tӱ�5^��#[hJ@�����!���"#�6�k��$L"�ح�3����X��&���/ޱ�� x�Ӹfȕ�C�d����Gn����m��L,:�BZ�3f�����c��m8%�����������D��1o�^䱥>ʉ��:գ� �ʊ����߾�#v�� ��;����䌆�C98�O��`wn��֢ib���~�v�b੤4V �� ��S���Q�f�����\��5E#(�(��O35����͘vN���0X>���KG�c%�d�Չd$���"nvG�d)N^W'��}��������K
�DY�"��V�*^��,)��*Y��3b=����S��C�9��v۾K�Z���������������=z9��gJ����3�1�BDԥ���:��a�)8�Y���sO�zmM���0����B<�8HB���`�j�w_p�[���gN�h�x'�g�N�K�19�b�$���R��XH�ީ?I:9�v�!艻���q7I���y����P~�_~�2���5�'\^�w!�X}�T��ve��%�?�se�C��#=ز���/lY�ȉ�=)^#'_t��笁�;��vuTr"�b�=��&v�;dY�ܺ>!f�I��֑ 
  �u���}�}Y���ͽ-2�F�੏�Ɗ�����ee<�P���
W���Y�XI)�^=������z���ْ��+��*�O�\��>1@ocw7TM-�j��構�����10�4�z��;0\�RB�Z�
Tr^�T��_�>��R�tK'HS�1��{��&f��v���	l
�Mϣĕr(R�M+�,��Z�S8��ހb��vB����\�b�aƛ������R�l��=^T��X̆`�N�H���ߧd}��U�]Ž��<��l�j;s�Xɇ��)�F�u���=l�\=|���Sroe��J�-��Z�`����͑�l'��"�vo�W)���:��>$�<�a����mP�XA�=/)f	=�@v�G$�r��7(�E�PxJ$MM��i	�(��b��^�%%���P
ʔ=�3�c
@�J�^�N �����t�bxr�#�M��i^?P�ĉs�w��NI�*%���v<�Z��}nx���m�8lT6�]~�$xzrT�_����7���l}b��R$���C�5]X>�̼�׀�0�����ߦ$A���-�ga���464�W��W�42f��tb����8��.��QCl;��i�V�M.$�4&�K�w��������Ani4&竳
�ېޱ�I儝������M�����!W��^	! ��W�~$NS$�K��r؆�!bg�%���xΥ'玍�a�3#s`�䬹��H��Ł����e�o�?Iּ���&�$jԉc�(��c}PiW��f��hDX�Om��_�:�`r^J6�	7z���h��Ҏ��9�������	Z�
hk���k�2�au���}���c�!���,15v��hT=7k��ydI���d��:Aj�#,j���&A�q��^1o
9�3����WŤ�����peY�,��`<&=Z�j�jWo3�K�?�%�396�qF,�D��m���W&p�K��d�}D��=Y�^?pR$���qs��F
X�pf��S�mT��(ѭ!�z�a������N����0k��JI��7(�˳�������~�n�����L
�`�����ic#~�#����DF��Bg���+�D��
XB���*��c�Zx�<��'�]+��u~}Y>V�;��*ɐ>��NfrN�_2"��3
� ��N��+S�KL5�O��~uX�	��Zb��W��
�O3?��v̴9a��L>YR������k�b��� <�(Y�ؙ	��F��i¾��T('�L%�����Wٚ��ݲ��/O2��`� ۝h��
��z�~�L�N��O>r K0�|΅Ūʄ���g<&=��j� %�zE��N؂��1��p���5���$]�M'ү)�ARBR�ٕ&�Ɉ�8H�S8l(%|��qi�TJ`���e0:%@��D:�D�I�t=���LJu@|�᏿���Ʌ�P��|mL�i��i�0
�s�`.�sCHJ�EE	gq��UOi�%]XC�ZJ��	Nɧ>II�:JLd��0XL�zN�8�*A)���X�ᘞ����bN���	qD9�D��U�g�:�n��ʉ�W�پw<?|<a���X�I������&�i�w]�+O�w�l�:�UUH<5�y�4�>�D��`๕�1v�5���v�&jAZjj�3��{TڀM�Z�է��ڽ���P��a��UF��S���Y�pa�AU0�y�+��^�4�.��WD��L (A��(VHV+ҮB��G�L���w⌍�`��[��
��O	B���}���h���� D\?�~$�)y�1��)q�(!Y��v&D��,����N�.ۓ�X�����-e'��E	�2��'RְQ��������<}f��.�sL=�\�pGB���]!��!1�����-���ۖ|,�������9���3xnߖU	�blGZ>�؎~)l��}�����9�#��ʌ�{�;��b;&���UzE��6ˣ�C��r��q3)q)��>�����'Q���A�V���5���'rR��pr�4��w��zGzB!V����A��xlڇ݈2�}(�������35B���3g����e{�^�u�;(���]��kWҽs�����m��P���-��^W�����#�\φ~�7��}�����,=����j�?�)�[ᣱ�����	�Kb�^�����<3���U�s"��נZ����}�k�lH�׏���O�����UR��C��`d��L0���j�F����D����nC�- t�8��_�=FN�ľ�x��Ui6��:>��*�R�}U�"r\�a���/�y�"ȺC+׭W�֠Q# ���v2V�!}:���j�i<��q�O��mkm=9�0����~ #L`�ܡYQ%%|�ں��-��~Q�[�M��I�c�,���e�.���H��NC����U�	���a�fg�W�x�C��!�c�C�3�~�V�Ly�~�z����ӮŎԶ��,L�\76��R�)?u�����و�PСf��[虇�!�U�w�x��U��`���eyY�+勊����bg�_TjD��X�v�Zk�~�1L< 3in����R������'�B�[Gԉ��&��]0O�Ks0�U�� �+ ��IØ}������^ň�mMҢ�3$2��維b�'�&-��b8��dfOl�
ɍM�oR��Pl3_���>li���`Z�6�u�d��8,)��8�y���g��Ձ�rN.lr���/p�n^n�HJ�����8����)�edq��M�C�30�dF��i�^$�M�L|�T�^��XC	��?���6!)�(#I0����[76i��H^9�½�K����'+H��L�D��`�ɤ ^=N�0�F�����L �F��������/��wO�����@�*�ݜ[�#�WT�����Fԁ�r�`����1=a �L�7N��q�"��L��
a�

��\��6�Qui۰��l�}F��ix�h�дeTp"����Z�5x������,�4^���!EP!~���x	�m�R�%�#�z�!ٿ�4ԇ ���B�@�F�.<lέ}��5%0X%"�N��h8�B�f����fI��m�$8k��w��b!�s{�yuu��o�^��^A,C��Q���n��wF����+k8Ѧq'��v�^�mnظ�񜅎�b`9)�a%��� ������x��	3�O�;YC������r8d�l��xo�'AN	6�S������i��P�����yO�x��7���u�y8      �   �  x�͗Ɏ7���S�h#h�V.}sd'Hx�=��h�5����^��n�����Dj�"���Z���a�(��}xML���t�n@�Q��������O�1�{�jO�`�7Ԉ:�=@?pP�fJZ�Ctп@���z�_�=�J��^�}�F[|W��i(mZ{�z��7Жm����#bO��@������yk��ꆏo���Ջ?�Î����Qdc^�b�
+	���sr��1J,���5����]�Ɛ�Q��l�$�:'���ѝ�J��8_CI�-��E���0E����(k��>R��7g=�RR�A*�ʴ��")�-�a�J����7�t*�-�s+�G��B�Òx�ՑV>o;���:�S��z9��e^������%h�#V ��\Nu�r�p�	GHd��K�>�GZ�e{6a5E�<E�7�EV�_bW�y>�-��ʔ�F/��(�!�f���^NO	�y�2U��)=Fe�}!�2~���-�.�E����M����f|1��HI΢�7���3z^pu1�0�\�5V��������3�߉֤��hE^��(/a��TQT�HeӔ�t�8ISd9�;�0_��,v�%�B+i
K��!���5?R*Y�xkk�����T��Dߏ���e�>���47�ANk�d
���=<����%%kh�B'W�y�Z�T��Sƾ�)����)a6�Ҵ5?����~kL�B.�NEx���V��,��5]�_PZ1�D��H�C�IA�D�kN7�L��ئ	�a�D�f�
3��^��� �
�E`GN>�TQ���$�C3EӺW5��2o�s�a�8�T����8=�L:!mE�$�9@+b�sw���r��X_�r�y���L�3+�*�hI�b��y��[_.x����aj{��8	<�y���ߝ����f�"��aCTB�NQc�8����i-���R�\���"��G��l��qSy      �      x������ � �      �      x�3�0�t�s��4���b���� 2��      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �   �  x�UV�r*9]���%�м�%�r/�P@M6�ݦ��mSn;)��G��c�RI|t$Kǒ��n��X%.�?��[ݧy�������T�[=�����t2%�7 ���Qy݇-��*u)lRK��K�[(�3G�yi
s3��B�o�P^Si%�S�F�A����$#+#��"i��t��O����<�/�c敲��ev�K읱e�������,,Vu�?Q�~k� ?%��Τ�U��`����Y��+ٙ��1�^��s�s��W� �3mΡ�&9�2��{�0i�ۈG���qǵ�e t���2f6";3�HG�kNc��օč( ���J4`e�Є���N*���#�]2���b�KYhqyH7��4I�U
U$(@'Y�-�~��iPq#Kat�ي"dݙ�
,W���U#��
�W���>���{-)�5h͡q�R���JGdXX�u�h���)�(%y�1ѢB[8�d'D�$�^k�8���e�[Iݠ9iOUs�gB�wIC��P��\M�K�-\���������O��(��{�Z�xz��;8����K�xj=[��������qR��R����� �ͅ�nV�{���-(���}���t�O�5|�po�^��[�9C� gh"1#����Oh�5fC,�l&�d�M�J�礽�&�c��l��F�ws@w�?2�;� �c�O����4vB�אX��5=Z��4jl�]3�!\�;�b�o���|փQ�
�,�c�����\�o�b0<�p/4��S�����Ej�$p���2,<Ҟ+�
ay���o��c�L���a�#8�'�'(����~P�$Hv�y�saIש:�D[�lk���B�Emb}�������aP�\t�me.�u����A���)�p���&�3ȶ��UMF�f��t"�Wpt��m���h'��ɽxp��J|Qx��d���s��0iMk�@�*����4>��a@=a�(�Vb(Fƌ�/� e��p�2�1a�Z��<�2���%�����ׇ+��*�/�8ᐹ�؎C)x+�3
Yj����8�q(c%�ö`$���ީ*�8�q�|�CZ^�!}bp�b����]�,d����$�/�o��K�����(4�B�;�G0�7�mː��[-���?陿���y46�o���H�6@�P�{�[g��+��)N�~Z��
F��ܻk2�5
00$ ��W#Zѐܞ�ɘ����a(k��������m���?===�����      �      x������ � �      �      x��[�rܸ�]����R��	��%��*�{H�<���ҽ*[�;J�{<_?@f���ʉ$���'�6I��$Q켋&�M��Ѥ^tq���|H>��ܺvVr:q�U���#���%�fM4=4�h���X�ޢ�,R%��(:�(K�I�Xt�&�S�̓n��y�R�&��"�ջcs�]̣��6��U{jjV*I��u��*��u���K�OrUH�&�l0��.k����4ᆺ��U�4K��-%�n��v�٘����ɪ�����2v�ɺR�*{�~��5��2����4HZ&�J�u�]��A�UJ�Q�J\&�L�O��[i&����b��&�&�c��ꆦ�eE*�E����+cpm�(�M�v߅@IV��Tl����á��;6�RU��&VU��Nc]������b�-d�n��)�3�qQ�b%����*?*2�J��N�V�Q�Ҟ�/T�7텴C�	j/�vc�q��Jb�D,5#i�R������HX�P���HhX��3_�c#V���Q����ƺ)h���nd��H#c!/zm?��[!�8䪴(nbm�����ߩO��Y�w�͕:p��[�hi���Q@���}��l����-� <���X�x�f�&Ɇ��������u��� �j]'��Ѭx�*�b䬼��RS��Yu�U �f����\k����*W7ޜZ$O��e����B�^+�	h'��+QjOK�k��thd�咐��8�r�6,�-��� j��<˝?#�(�+�>'W`���V�<����
Q�7�e��u.)2/E��;8.T9���DT-:6S>�c�˪�%R��U%Jp� �/]n���5; w�`�P/U)a[����Ɖ#��E���<�;�bZ�r�ǦM�A�%8SJ"(2��EƱU���vi��ǧ��*)b�c� i�H<�f�a�%�U��v^S��q�ĹA'��
ZY�s����g�Q����m�+��rw�p=��JJ�X)ˀ�v��vx����7v���4RdI��_p�)͝��B�XJTf��-6���bwT�@��\�N�t��C���J��j6��K-�.K�=ug���Ԉ�K�����[�?��:�K�˂�D8��njJ�
��1ȲXV�2=B��&G0�zSIw�����\LDU
�uǌ̣�K��갵�L�1�RAB\��֎4F�^�T��*2�~���')���6 ���%�w�W�������9���.���� D���2f`T&y�	PO0�҆I���d���F�W+x�F����6��+d���kfԪ��!���˓��tK�5v)�2L(�W�\�ҵ2>Ǎ�釦E��qN@��E�	�;���y2�+�Q3dkǶ?s;�6X ���]M�$G�5f;i�����JQ����hϟ����t�X�Z���
��>�H3�k�Z�)�S.�4�Ό6p&K�Yi�t(����p��T>�\��)p���'�ڮ�O����.H��U�(k���[ʅaHp�gQKY�VR:f�c&�9�fxM��k�!��
Q k�:;e�ec�p�sn�����������-��.'mSo V[�{G!S��VY邭|j�2j����0b=FE���:,�l	,Y:�ƅ]�3��<f�H���N["SqY����i4�2JKrL��;����  ����̧y�&�l�8q�mRD<S2���F��[�k`�!����u!�g�v�x47\*T*)���jrkۓ3z#8��`H" ׷�zѭ�tA&���[�9���5���H��F!�	j�6�}��ɬ1��ei�m�}A'*� �ϔ��5�k����ydv��=���ܰ}�)�V��l9�Qy"�
q� t�-�M�l��^T�W���bP1�r��#J�i��d�{��/2�G=[��&M��H3ue(�+1g�4ˮ�%��+�a1��؜���|��%ZP�"�%`Lv����b�(�ѲA�O䉸T,K7��u>����3h?�ۢ�M�s�q����M��t�+B��V �N��)HN._ay�%�Z��и��He�*Hw�]Ś�2R_*�8����iݴ6�ϙb��r_,�(��w ��d_�����mJ��8�n0�<������.��eW��v�p$�E:F�rĔ�zx%�!x�v�:	��/H���]s����d�� >�_Q$�x�^������R���5j�$����sXl���#K2N}�C�EK}e�b2�=n�\VJ�U�2B1j�rj�$c��ض�޶��}с�1f�‬��qU�)"��A���ʍ�۠WU!�������CԝY�/-�pة�[�5�����q�.���[�$��q%�����}�k��-0��f*Z��n�kZ:�2�v�PH���"ߚd�9������!�ll�G(dh��;�` 1�rx�F���^���-�x'�d���YZ���?|_�Y��5h�C����߱8ː�|�����Q'i%#�E&���~C�"f�'
*!+u-��Jґ��1}Ə򤖏�P��b��l����\`/�x�L$�؊���˔q��u�_�x��Yv~�r��ZL�i�H�4f�U���8�N;w���\�=JC)T��P�����,��n���X���!X��ɶv@�i钎�_���Q^M�1^yJ
�Ê�`өD� %c���hQ��p����� 3��Z*O?�*k�V�ΝҸ)˹�R���gQ�D��\�9K�B�9T9(T1���R� ���lp<�n;���#-+�ٖ�3R*M��l�c�P:	y�|�0]�n��+�!�<5��졿�Y�uL�{>(P�!���R��MW ��T��BI.���}�e��	4��m�ї���ײ�#@���LJJ�Q��[ϥA٫|=�>,Q,򹀜�)�p�8�
����y��[C���4���-��-r�Lw�p\����)�n��b<�X�,�V3T������Ю}�~�;�4��Ο$/-�%��8^������_�����~�\P� �w{��nv��cS�\��m4;쎇���D%ѻO�Đ6�=#��}Hù�t��l+y��:��]DX����g��V���E*���Ը"�d�%�'�h?���+�8Aw�;l���)��~TA{�<!2��i�|�������閏�����lX���[��Q2#%�	��CΌ��#�'N���^�"7w�o49��O����<�gzP�=�_�Ǐs��%mzڧ|�@�!�D�(�<���/��W�c��b�R��)���];�F����~���<�!���»�������[���	���U3���������1j��P����x�7ʓA�4�5�6�G��?_�<(ڸ�tg���H��,R���7Ir����\��3+���q�V���z�AW��x*Ξ�H�m�[�t��<�P�SB�s�:d��|��{X�7���9a_��G-�P�Ma7(z��Q�S�f�4��X{�1��5(3ǰ�C����0!yF;�M��%l��y,S7�Yg�=��g�(��>	'�$d�4tVzro;@x
$`��dġ"�^����� �����i�R"�?�q�OC3'L�}v	qg��<Le���F����q��j���0��/�+�3:)��03~�`Ғ���I��P�Gfa�i�$�ܛ�B����������*H����O�m�������~z��İ�^��z���m~
�j�1%�P�6<;>]~~�m���;�1�)Qn����E��]$\P>�w�u�'�̆챻�=����3�����~�W$�������N7��ǜ˩�5���`$�����3i�ן������%"5��?������,����bѩx"ZVX]�M������qIp�p~����K��zH,1�/�R�
m�BS�������\ ��7����g��K�7Q�7�7S$�8�$ڳC0�usp��J�>ET�־VN������0��љ�f��e�(�������w��f�޿q�L�M#/]4s�Y�Z���"���X�ʧW��s�D�	�d� 	s@������N�k�c��J�]&��]o/����	���@���+��S���>��U�K��_�^\p�~��ܺ#]+7N�*�?�@Ͳ���/�����w I  �E�����r<�g;�����,0� Mb�ʲi��H�d�-�xԾ��/���Hk7�Cl��8,����b*Kf�<pp��N�s���q؀����hz���&�f*�� >Mg{�֒uA�byZs��Ë��ç"+�)�Az�~�G_�3R����μt�����ݕ�p>=��X�ԅ.Lcy�K:z�}�*]�=�����r����"�������ex}:��^��C
,'��owO��Yz��p��4d�~v�Hf��eT����2�� �>���+A%s~����O��m�'~���?�?��w��M��������'�����+f��\����7$��n�A"�-��E�����,�&���![��dwT���Me���J������6�z4�\6;����:_����'M�����OkCc�����g����<�r{�㼟%ZD��8:�۽��|w�e�5��ւ[�?k��������|qqr���˙��<�u�����L-r�k�kp~�3���i������ of݋E_3��9�K��r�VIVN����۷�鉙E����v??Y ��s��o?��
����i/_ߞI�/Q���������LDS�wpD|S�b���	�]��ow�L�~w@��{�u��,g��m�[i��ۺ���D��������@ ѓG�� M��v���,b��mfX��Ғ��go����˃@��G: ��8+�#
����E��*8��k�B:1����p�*�'�Y^���Kí��H�p50S�$K�/+ڰ�y'�9O��/o�¯�d�z��S�9a�쟾���D�n�g��h�_ή��|�:��`ǅ��c��]�gn?��7���ʩ�ߔ�����p�yӍ-�j���#]6du��\��~�y��I!(�|��_��7	ay�p�N��ҽ	.�@7����pu�_G��Á<�;�(��+�M��>G����o/�3Mj��' 4����������e�R=�2�0�����DT��D�n�B}�k�m��Uu-OYn��T2M~?O8���ouD	_��N�B��<:!3g�����3�x�j+S�,-�tݴ)�R���01���9:���>Fajo�Dd|ch�T�gJ�mB�⧤݋D��Wӟ�����n�Bq��'x��4���d��bҭ�n�����K+�>�g��/> ��O�I�+���A�n�q��$�W��S-W��-�S+��:^�!�����LnF���Pr�i�ɇ:t9���]6,3C�b� �Y�7ۮ�uG�+�6I�S���>Ħ�x��d�X�l�Pa�v*�K(=u&�%!��Oy���'��l�ef��s^��z�r}oJ5����iۺ��8S\�w`�]����E;ԝƟCޞ��F�R���;��ӿ��4��Cn�(�*�1�mP�S��wl�J���K~+P�ي��L���)��1������#G��5�J�{�-��=	�Ɠx��ۥ;�d)�愘�H/��\�1:�3�(�J	&�F�\�ǽ� `�<]�Tѣ�k�Ҁ~A�Z۬OrGj����E,�Y,5�	7����pvi��?Y�����_~���ݜs      �   :   x�%���0 �w���v���D�u�z!�w���F�"Ɲ�taf7�{��Z��      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �   �   x�����0D��+�W�l
�ؘ�С*k��C��I%����|����?��0��3_���~Wl�)�3L�GL0T�t"�0=�7۰`zC�j���5S��)F`Z1���~�0�vwf���a�-c5��Τ�355�nŗ�:��ē���R���U���*q'Hޙ �Ö�<|�#5�x܊��/a�&kv�dז )SHwɻ"�69�J�욢��)?n��/(�̗      �   T	  x�͜M���Ϛ_���4�X�ߒ�l�L�]L��e/�`��.�C�}��(K4m�V5����v����x�X�o���O�o��}�pD<�;~:�~��O�|g-{���_?�����Χ�1� �a�f�� FJ~@��4|��_��s�|�ӗ������<b�"c~o��Wra��/?~�H~�4��̫	P����}�|r�d�o�/�G������d�X���� Y{� ���p�]y6���o�dc��o[,`�"QHj,#�ë���,��-��s��|�C!\(x�)��#:��A�0 � �\���m0X��A��i28�C2�j�����$t�����ƀ��(�����>8���^T�5b�@�X�pnxuѯ,�y�A��t�a	X0X�H�R��3jcȑ�� 1"JܴGC����cx��2�;8~<�"&$H�vx�p 	��G��l2����s'���� �0��1����P"?c�J���!0sI�8'w��=9w�'�y}]
~���\S�)�T(�x�|�,#`xM�U��_B���_��P�����*E���,�η(>(�Ɔ�tX����YԹXON����� �,�&�LFl���0��c(a�s����׏A��*\��>�!\���hHFI�p]dv�'q� ǜ8X��q����s�pK�p�(�3ωB�i�1�38Q�2#�&�
D���
��cڴs� �h7A8�J�����g��~�Hd	a\Lb�����T�T\<�A������ڕ�`�X�BS�<	�%+��CjY�n#yV[�!8��]e�0C���*R��m�9�J�3�#��-�x����</�,�d(�_	t'�/���+<�ک%G[�&<��Ӷ���ބ�;�XJ
�x��M~")�	������4���k��z�L��g��F�^4����Z��h}K�W���$�,�3pR��"H��'9�R)Tc��}�M�Y��:-���0^�^w���͛U� х'�'H��@V��C"��M4��$*-���\��7.�m��Ap����-�(wv
��MTN��Ңɇ���0��!�D�r˭eg��=�u��[-��B�ās7+:e���C��Fn��>�v�65b��d"OD\	.Q�(����'n�G#d!�U��mA�.���	X���{j�w����p�j4J4�������#���Eه`���}(T b�/>ɍ8�N��pX�Г�k��O uU��5����b ������rKY+�L���=.U$ҝ��%	H�}��#�P�yUdkp{�b�I D�� D�F]�����nok*9�!m��=1^���;\g8搤��5��+ǐ���'U���pGy�	����f�~�ȷd�rB��$t�E�Ush �M
�<_S�!w�E�V�pZ�A��X�ZV����b�����p*�Il��h�@R���F�X���B8�G8gQg�n�N�fp#w�[^>�nM�ֺX�uy]�,:���	N'��SINJ��gr ����7�)�9R�τ���I�V*���6o�����F@]�c��=�3<Q<Yi�]ExJ4�)�(�pZ���p�b?eA��������W�5����0z��M<{��H���m8�i�n��J�25��'�Ɨ�����L�cIӤ�W���,1f;%3��h-Rr�7���$]�f����WR���R��#,v�w�/( V�����In-�#��}��%Lr�A�� y7�0�� �)H^�T�}��Q"g >��1Z(�/����h�� �J59h�X6�����厙�E�\�MA}�y/��\�r�A4��� ����
l�)ĚB��Oy�B�r(�CFȺR�!�p ���a�!��
��.h0@��mEB�6	�3���(p1��j�M�k����7b�
	V�+�� K������c6�1�BP��q����Ly*ïe�<�x!�(0.(&�X9sH�%�����Vuqv������([����ܸ��
(���Ք嵯t�'ll*�;�1Z��1_�{M�~g�D4^;mT�2x�0
��~#B��M�K�0��6Q)�[!��m"^�:YdCTe���i˙;�-b%-]��F�CGθ�\K�x>\��hoBiD��Uv߅ۅM�ٴfQ���ߛ������Gf�o�	��e4���;m�!������Q�'O�k�f*	k�,�5��A*ݩ��k
�m��W�p'�t[u�7,&t�h��<Bp5��p�Ua���や����d�rQ��[c|�1,���[y������JgpJ�������+7�8F���֙�sQ��.G|���� ����L�/�$S����D�7�޽�k{�     