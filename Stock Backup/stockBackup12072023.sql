PGDMP          (                {            stock    15.3    15.3 ]    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    33473    stock    DATABASE     x   CREATE DATABASE stock WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_India.1252';
    DROP DATABASE stock;
                postgres    false            �            1255    41824    createotp() 	   PROCEDURE     `  CREATE PROCEDURE public.createotp()
    LANGUAGE plpgsql
    AS $$
BEGIN
UPDATE "Transaction_OTP" SET "IS_ACTIVE"=0 WHERE "FARMER_ID"=FARMER_ID ;
	INSERT INTO "Transaction_OTP" ("FARMER_ID", "VCHMOBNO", "OTP_NO", "CREATED_DATE", "EXPIRED_DATE","UPDATED_IP") 
VALUES (FARMER_ID, MOB_NO, OTP, GETDATE(), DATEADD(MINUTE,10,GETDATE()),UPDATED_BY);
END
$$;
 #   DROP PROCEDURE public.createotp();
       public          postgres    false                       1255    41823 !   createotp(text, text, text, text) 	   PROCEDURE     �  CREATE PROCEDURE public.createotp(IN farmer_id text, IN otp text, IN mob_no text, IN updated_by text DEFAULT NULL::text)
    LANGUAGE plpgsql
    AS $$
BEGIN
UPDATE "Transaction_OTP" SET "IS_ACTIVE"=0 WHERE "FARMER_ID"=FARMER_ID ;
	INSERT INTO "Transaction_OTP" ("FARMER_ID", "VCHMOBNO", "OTP_NO", "CREATED_DATE", "EXPIRED_DATE","UPDATED_IP") 
VALUES (FARMER_ID, MOB_NO, OTP, GETDATE(), DATEADD(MINUTE,10,GETDATE()),UPDATED_BY);
END
$$;
 e   DROP PROCEDURE public.createotp(IN farmer_id text, IN otp text, IN mob_no text, IN updated_by text);
       public          postgres    false            �            1255    33572    hello_world() 	   PROCEDURE     �   CREATE PROCEDURE public.hello_world()
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
       public         heap    postgres    false            �            1259    41841    Price_SourceMapping    TABLE     �   CREATE TABLE public."Price_SourceMapping" (
    "RECEIVE_UNITCD" character varying(4) NOT NULL,
    "PRICE_RECEIVE_UNITCD" character varying(4) NOT NULL,
    "SEASSION" character varying(10) NOT NULL,
    "FIN_YR" character varying(10) NOT NULL
);
 )   DROP TABLE public."Price_SourceMapping";
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
       public         heap    postgres    false            �            1259    41846    Stock_Users    TABLE       CREATE TABLE public."Stock_Users" (
    "UserID" character varying(50) NOT NULL,
    "Passwd" character varying(100),
    "Passwd2" character varying(100),
    "Passwd3" character varying(100),
    "User_Type" character varying(4),
    "Date_Create" timestamp with time zone,
    "Last_Pwd_Change" timestamp with time zone,
    "Login_Attempt_Fail" integer,
    "Login_Attempt_Last" timestamp with time zone,
    "Login_Success_Last" timestamp with time zone,
    "Login_Status" character varying(10),
    "SessionID" character varying(50)
);
 !   DROP TABLE public."Stock_Users";
       public         heap    postgres    false            �            1259    41810    my_sequence    SEQUENCE     t   CREATE SEQUENCE public.my_sequence
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 "   DROP SEQUENCE public.my_sequence;
       public          postgres    false            �            1259    41826    TOTPFARMERSALE    TABLE     �  CREATE TABLE public."TOTPFARMERSALE" (
    "TOTP_ID" integer DEFAULT nextval('public.my_sequence'::regclass) NOT NULL,
    "FARMER_ID" character varying(50),
    "VCHMOBNO" character varying(50),
    "UPDATED_ON" timestamp with time zone,
    "UPDATED_BY" character varying(50),
    "UPDATED_IP" character varying(50),
    "MOBSTATUS" character varying(1),
    "FIN_YEAR" character varying(10),
    "SEASON" character varying(10),
    "CNT" integer
);
 $   DROP TABLE public."TOTPFARMERSALE";
       public         heap    postgres    false    237            �            1259    41831    transaction_otp_sq    SEQUENCE     {   CREATE SEQUENCE public.transaction_otp_sq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.transaction_otp_sq;
       public          postgres    false            �            1259    41832    Transaction_OTP    TABLE     �  CREATE TABLE public."Transaction_OTP" (
    "OTP_ID" integer DEFAULT nextval('public.transaction_otp_sq'::regclass) NOT NULL,
    "FARMER_ID" character varying(50),
    "VCHMOBNO" character varying(50),
    "OTP_NO" character varying(50),
    "CREATED_DATE" timestamp with time zone,
    "EXPIRED_DATE" timestamp with time zone,
    "UPDATED_IP" character varying,
    "IS_ACTIVE" boolean DEFAULT true,
    "OTP_RESPONSE" character varying(1)
);
 %   DROP TABLE public."Transaction_OTP";
       public         heap    postgres    false    240            �            1259    41830    Transaction_OTP_SQ    SEQUENCE     }   CREATE SEQUENCE public."Transaction_OTP_SQ"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 +   DROP SEQUENCE public."Transaction_OTP_SQ";
       public          postgres    false            �            1259    33489    mCrop    TABLE     �   CREATE TABLE public."mCrop" (
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
    public          postgres    false    225   �       �          0    41841    Price_SourceMapping 
   TABLE DATA           o   COPY public."Price_SourceMapping" ("RECEIVE_UNITCD", "PRICE_RECEIVE_UNITCD", "SEASSION", "FIN_YR") FROM stdin;
    public          postgres    false    242   4�       �          0    41735    STOCK_BAGSIZE 
   TABLE DATA           N   COPY public."STOCK_BAGSIZE" ("BAG_SIZE", "BAG_NAME", "IS_ACTIVE") FROM stdin;
    public          postgres    false    226   ��       �          0    41740    STOCK_DEALERSTOCK 
   TABLE DATA           �  COPY public."STOCK_DEALERSTOCK" ("STOCK_ID", "LICENCE_NO", "CLASS", "RECEIVE_UNITCD", "MOU_REFNO", "CROPCATG_ID", "CROP_VERID", "CROP_ID", "SEASSION", "FIN_YR", "LOT_NO", "BAG_SIZE_IN_KG", "RECV_NO_OF_BAGS", "AVL_NO_OF_BAGS", "PRICE_QTL", "SUBSIDY_QTL", "STOCK_DATE", "STOCK_QUANTITY", "AVL_QUANTITY", "USER_TYPE", "ENTRYDATE", "USERID", "USERIP", "ENTRY_STATUS", "TESTING_DATE", "EXPIRY_DATE", "VALIDITY") FROM stdin;
    public          postgres    false    227   ګ       �          0    41745    STOCK_FARMER 
   TABLE DATA           I  COPY public."STOCK_FARMER" ("FARMER_ID", "TRANSACTION_ID", "CROPCATG_ID", "CROP_ID", "CROP_VERID", "CROP_CLASS", "Receive_Unitcd", "LOT_NUMBER", "BAG_SIZE_KG", "NO_OF_BAGS", "TOT_QTL", "ADMISSIBLE_SUBSIDY", "PRICE_QTL", "ALL_IN_COST_AMOUNT", "SCHEME_CODE_GOI", "TOT_SUB_AMOUNT_GOI", "SCHEME_CODE_SP", "TOT_SUB_AMOUNT_SP", "SUBSIDY_AMOUNT", "SEASON", "FIN_YEAR", "UPDATED_BY", "UPDATED_ON", "USER_TYPE", "USERIP", "TRN_TYPE", "XML_Status", "RECOVERY_AMT", "RECOVERY_DATE", "RECOVERY_STATUS", "GOI_QTY", "SP_QTY", "VARIETY_AGE", "PREBOOKING_AMT", "PREBOOKING_APPLICATIONID") FROM stdin;
    public          postgres    false    228   �       �          0    41752    Stock_Agencies_Master 
   TABLE DATA           g   COPY public."Stock_Agencies_Master" ("AgenciesID", "AgenciesName", "IsActive", "Priority") FROM stdin;
    public          postgres    false    229   ��       �          0    33514    Stock_District 
   TABLE DATA           `   COPY public."Stock_District" ("Dist_Code", "Dist_Name", "NDist_Code", "LGDistrict") FROM stdin;
    public          postgres    false    220   ��       �          0    33519    Stock_Godown_Master 
   TABLE DATA           �   COPY public."Stock_Godown_Master" ("Dist_Code", "Godown_ID", "Godown_Name", "User_Type", "IsActive", "PrebookingGodown") FROM stdin;
    public          postgres    false    221   �       �          0    33544    Stock_Pricelist 
   TABLE DATA           Z  COPY public."Stock_Pricelist" (slno, "Crop_class", "RECEIVE_UNITCD", "Crop_Vcode", "Crop_Code", seasons, "All_in_cost_Price", "SCHEME_CODE_GOI", "SCHEME_CODE_GOI1", "SCHEME_CODE_GOI2", "GOI_Subsidy", "SCHEME_CODE_SP", "STATEPLAN_Subsidy", "TOT_SUBSIDY", "FARMER_PRICE", "Date_of_entry", "F_Year", "VARIETY_AFTER_10YEAR", "IS_ACTIVE") FROM stdin;
    public          postgres    false    223   ;�       �          0    33554    Stock_ReceiveDealer 
   TABLE DATA           L  COPY public."Stock_ReceiveDealer" ("RCV_NO", "LIC_NO", "RECEIVE_DATE", "DD_NUMBER", "CASH_MEMO_NO", "GODOWN_ID", "RECEIVE_UNITCD", "CROP_VARIETY_CODE", "CROP_ID", "CROP_CLASS", "LOT_NO", "BAG_SIZE", "NO_OF_BAGS", "AMOUNT", "SEASSION_NAME", "FIN_YR", "USER_TYPE", "STATUS", "ENTRYDATE", "USERID", "USERIP", "PACSRebate") FROM stdin;
    public          postgres    false    224   ,2      �          0    41757    Stock_ReceiveDetails 
   TABLE DATA           �  COPY public."Stock_ReceiveDetails" ("RECVTRANSID]", "Dist_Code", "Godown_ID", "AgenciesID", "Receive_Unitcd", "MOU_REFNO", "Outagency_Waybillnum_Farmnm", "Challan_No", "CropCatg_ID", "Crop_ID", "Crop_Verid", "Class", "Lot_No", "Bag_Size_In_kg", "Recv_No_Of_Bags", "Recv_Date", "Recv_Quantity]", "SEASSION_NAME", "FIN_YR", "User_Type", "EntryDate", "UserID", "UserIP", "TESTING_DATE", "EXPIRY_DATE", "FARMER_ID", "STATUS") FROM stdin;
    public          postgres    false    230   I2      �          0    33526    Stock_Receive_Unit_Master 
   TABLE DATA           �   COPY public."Stock_Receive_Unit_Master" ("AgenciesID", "Receive_Unitcd", "Receive_Unitname", "IS_OAIC", "IS_OSSC", "IS_ACTIVE", "SHORT_NAME") FROM stdin;
    public          postgres    false    222   f2      �          0    41764    Stock_SaleDetails 
   TABLE DATA           �  COPY public."Stock_SaleDetails" ("SALETRANSID", "SUPPLY_TYPE", "CREDIT_BILL_NO", "mDATE", "DEPT_TYPE", "GODOWN_ID", "SALE_DATE", "SALE_TO", "DD_NUMBER", "DD_AMOUNT", "CASH_MEMO_NO", "PRICE_QTL", "AMOUNT", "CROPCATG_ID", "CROP_ID", "CROP_VERID", "CLASS", "Receive_Unitcd", "MOU_REFNO", "LOT_NUMBER", "BAG_SIZE_KG]", "SALE_NO_OF_BAG", "CONFIRM_STATUS", "STATUS", "SEASONS", "F_YEAR", "UPDATED_BY", "UPDATED_ON", "USER_TYPE", "USERIP", "IS_ACTIVE", "PREBOOKING_AMT", "PREBOOKING_APPLICATIONID") FROM stdin;
    public          postgres    false    231   �3      �          0    33474    Stock_StockDetails 
   TABLE DATA           �  COPY public."Stock_StockDetails" ("Stock_ID", "Dist_Code", "Godown_ID", "CropCatg_ID", "Crop_ID", "Crop_Verid", "Class", "Receive_Unitcd", "MOU_REFNO", "Lot_No", "Bag_Size_In_kg", "Recv_No_Of_Bags", "AVL_NO_OF_BAGS", "Stock_Date", "Stock_Quantity", "Avl_Quantity", "SEASSION_NAME", "FIN_YR", "User_Type", "EntryDate", "UserID", "UserIP", "Entry_Status", "TESTING_DATE", "EXPIRY_DATE", "VALIDITY") FROM stdin;
    public          postgres    false    214   4      �          0    41846    Stock_Users 
   TABLE DATA           �   COPY public."Stock_Users" ("UserID", "Passwd", "Passwd2", "Passwd3", "User_Type", "Date_Create", "Last_Pwd_Change", "Login_Attempt_Fail", "Login_Attempt_Last", "Login_Success_Last", "Login_Status", "SessionID") FROM stdin;
    public          postgres    false    243   +4      �          0    41826    TOTPFARMERSALE 
   TABLE DATA           �   COPY public."TOTPFARMERSALE" ("TOTP_ID", "FARMER_ID", "VCHMOBNO", "UPDATED_ON", "UPDATED_BY", "UPDATED_IP", "MOBSTATUS", "FIN_YEAR", "SEASON", "CNT") FROM stdin;
    public          postgres    false    238   ؽ      �          0    41832    Transaction_OTP 
   TABLE DATA           �   COPY public."Transaction_OTP" ("OTP_ID", "FARMER_ID", "VCHMOBNO", "OTP_NO", "CREATED_DATE", "EXPIRED_DATE", "UPDATED_IP", "IS_ACTIVE", "OTP_RESPONSE") FROM stdin;
    public          postgres    false    241   _�      �          0    33489    mCrop 
   TABLE DATA           Y   COPY public."mCrop" ("Crop_Code", "Category_Code", "Crop_Name", "IS_ACTIVE") FROM stdin;
    public          postgres    false    216   �      �          0    33479    mCropCategory 
   TABLE DATA           X   COPY public."mCropCategory" ("Category_Code", "Category_Name", "IS_ACTIVE") FROM stdin;
    public          postgres    false    215   ��      �          0    33495    mCropVariety 
   TABLE DATA           l   COPY public."mCropVariety" ("Variety_Code", "Crop_Code", "Variety_Name", "IS_ACTIVE", "STATUS") FROM stdin;
    public          postgres    false    217   �      �          0    33502    mFINYR 
   TABLE DATA           9   COPY public."mFINYR" ("FIN_YR", "IS_ACTIVE") FROM stdin;
    public          postgres    false    218   o�      �          0    41771    mLATESTNEWS 
   TABLE DATA           V   COPY public."mLATESTNEWS" ("NEWS_ID", "NEWS", "UPDATED_ON]", "IS_ACTIVE") FROM stdin;
    public          postgres    false    232   ��      �          0    41778    mMAX_SUBSIDY 
   TABLE DATA           �   COPY public."mMAX_SUBSIDY" ("CROP_CODE", "MAX_SUBSIDY", "FIN_YEAR", "SEASON", "UPDATED_ON", "IS_ACTIVE", "GOI_SUBSIDY", "SP_SUBSIDY") FROM stdin;
    public          postgres    false    233   ��      �          0    41783    mPACS_DISCOUNT 
   TABLE DATA           �   COPY public."mPACS_DISCOUNT" ("CROP_CODE", "SEASSION", "FIN_YR", "TRAN_CHARGE", "OSSC_DEALER", "OSSC_PACS", "IS_ACTIVE", "LOADING_UNLOADING", "DEALER_COMISSION") FROM stdin;
    public          postgres    false    234   ��      �          0    41788    mSCHEME 
   TABLE DATA           ]   COPY public."mSCHEME" ("SCHEME_CODE", "SCHEME_NAME", "MAP_CODE", "SCHEME_CODE1") FROM stdin;
    public          postgres    false    235   �      �          0    33508 	   mSEASSION 
   TABLE DATA           u   COPY public."mSEASSION" ("SHORT_NAME", "SEASSION_NAME", "FIN_YR", "IS_ACTIVE", "START_DATE", "END_DATE") FROM stdin;
    public          postgres    false    219   -�      �          0    41797    prebookinglist 
   TABLE DATA           �  COPY public.prebookinglist ("bookingID", "applicationID", "bookingType", "distID", "blockID", "gpID", "beneficiaryType", "beneficiaryId", fin_year, "Season", "dealerId", "cropCode", "varietyCode", "monthOfPurchase", "bagSize", "noOfBag", quantity, "totalCost", "preBookingAmt", "totalPaidAmt", "dateofPurchase", "dateofInsert", "IS_ACTIVE", "TRANSACTION_ID", dateofcancel, canceledby, remarkbydeler, cancelstatus, "noofBagSale", "saleAmount") FROM stdin;
    public          postgres    false    236   7�      �           0    0    Transaction_OTP_SQ    SEQUENCE SET     C   SELECT pg_catalog.setval('public."Transaction_OTP_SQ"', 1, false);
          public          postgres    false    239            �           0    0    my_sequence    SEQUENCE SET     :   SELECT pg_catalog.setval('public.my_sequence', 20, true);
          public          postgres    false    237            �           0    0    transaction_otp_sq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.transaction_otp_sq', 4, true);
          public          postgres    false    240                       2606    41845 ,   Price_SourceMapping Price_SourceMapping_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Price_SourceMapping"
    ADD CONSTRAINT "Price_SourceMapping_pkey" PRIMARY KEY ("RECEIVE_UNITCD", "SEASSION", "FIN_YR");
 Z   ALTER TABLE ONLY public."Price_SourceMapping" DROP CONSTRAINT "Price_SourceMapping_pkey";
       public            postgres    false    242    242    242            �           2606    41739     STOCK_BAGSIZE STOCK_BAGSIZE_pkey 
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
       public            postgres    false    228    228                        2606    41756 0   Stock_Agencies_Master Stock_Agencies_Master_pkey 
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
       public            postgres    false    224    224    224    224    224    224    224    224                       2606    41763 .   Stock_ReceiveDetails Stock_ReceiveDetails_pkey 
   CONSTRAINT     |   ALTER TABLE ONLY public."Stock_ReceiveDetails"
    ADD CONSTRAINT "Stock_ReceiveDetails_pkey" PRIMARY KEY ("RECVTRANSID]");
 \   ALTER TABLE ONLY public."Stock_ReceiveDetails" DROP CONSTRAINT "Stock_ReceiveDetails_pkey";
       public            postgres    false    230            �           2606    33533 8   Stock_Receive_Unit_Master Stock_Receive_Unit_Master_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Stock_Receive_Unit_Master"
    ADD CONSTRAINT "Stock_Receive_Unit_Master_pkey" PRIMARY KEY ("Receive_Unitcd");
 f   ALTER TABLE ONLY public."Stock_Receive_Unit_Master" DROP CONSTRAINT "Stock_Receive_Unit_Master_pkey";
       public            postgres    false    222                       2606    41770 (   Stock_SaleDetails Stock_SaleDetails_pkey 
   CONSTRAINT     u   ALTER TABLE ONLY public."Stock_SaleDetails"
    ADD CONSTRAINT "Stock_SaleDetails_pkey" PRIMARY KEY ("SALETRANSID");
 V   ALTER TABLE ONLY public."Stock_SaleDetails" DROP CONSTRAINT "Stock_SaleDetails_pkey";
       public            postgres    false    231            �           2606    33478 *   Stock_StockDetails Stock_StockDetails_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Stock_StockDetails"
    ADD CONSTRAINT "Stock_StockDetails_pkey" PRIMARY KEY ("Stock_ID", "Crop_Verid", "Lot_No", "User_Type");
 X   ALTER TABLE ONLY public."Stock_StockDetails" DROP CONSTRAINT "Stock_StockDetails_pkey";
       public            postgres    false    214    214    214    214                       2606    41850    Stock_Users Stock_Users_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public."Stock_Users"
    ADD CONSTRAINT "Stock_Users_pkey" PRIMARY KEY ("UserID");
 J   ALTER TABLE ONLY public."Stock_Users" DROP CONSTRAINT "Stock_Users_pkey";
       public            postgres    false    243                       2606    41840 $   Transaction_OTP Transaction_OTP_pkey 
   CONSTRAINT     l   ALTER TABLE ONLY public."Transaction_OTP"
    ADD CONSTRAINT "Transaction_OTP_pkey" PRIMARY KEY ("OTP_ID");
 R   ALTER TABLE ONLY public."Transaction_OTP" DROP CONSTRAINT "Transaction_OTP_pkey";
       public            postgres    false    241            �           2606    33485     mCropCategory mCropCategory_pkey 
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
       public            postgres    false    218                       2606    41777    mLATESTNEWS mLATESTNEWS_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public."mLATESTNEWS"
    ADD CONSTRAINT "mLATESTNEWS_pkey" PRIMARY KEY ("NEWS_ID");
 J   ALTER TABLE ONLY public."mLATESTNEWS" DROP CONSTRAINT "mLATESTNEWS_pkey";
       public            postgres    false    232                       2606    41782    mMAX_SUBSIDY mMAX_SUBSIDY_pkey 
   CONSTRAINT        ALTER TABLE ONLY public."mMAX_SUBSIDY"
    ADD CONSTRAINT "mMAX_SUBSIDY_pkey" PRIMARY KEY ("CROP_CODE", "FIN_YEAR", "SEASON");
 L   ALTER TABLE ONLY public."mMAX_SUBSIDY" DROP CONSTRAINT "mMAX_SUBSIDY_pkey";
       public            postgres    false    233    233    233            
           2606    41787 "   mPACS_DISCOUNT mPACS_DISCOUNT_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."mPACS_DISCOUNT"
    ADD CONSTRAINT "mPACS_DISCOUNT_pkey" PRIMARY KEY ("CROP_CODE", "SEASSION", "FIN_YR");
 P   ALTER TABLE ONLY public."mPACS_DISCOUNT" DROP CONSTRAINT "mPACS_DISCOUNT_pkey";
       public            postgres    false    234    234    234                       2606    41792    mSCHEME mSCHEME_pkey 
   CONSTRAINT     a   ALTER TABLE ONLY public."mSCHEME"
    ADD CONSTRAINT "mSCHEME_pkey" PRIMARY KEY ("SCHEME_CODE");
 B   ALTER TABLE ONLY public."mSCHEME" DROP CONSTRAINT "mSCHEME_pkey";
       public            postgres    false    235            �           2606    33513    mSEASSION mSEASSION_pkey 
   CONSTRAINT     q   ALTER TABLE ONLY public."mSEASSION"
    ADD CONSTRAINT "mSEASSION_pkey" PRIMARY KEY ("SEASSION_NAME", "FIN_YR");
 F   ALTER TABLE ONLY public."mSEASSION" DROP CONSTRAINT "mSEASSION_pkey";
       public            postgres    false    219    219                       2606    41803 "   prebookinglist prebookinglist_pkey 
   CONSTRAINT     z   ALTER TABLE ONLY public.prebookinglist
    ADD CONSTRAINT prebookinglist_pkey PRIMARY KEY ("bookingID", "applicationID");
 L   ALTER TABLE ONLY public.prebookinglist DROP CONSTRAINT prebookinglist_pkey;
       public            postgres    false    236    236            �      x������ � �      �   y  x�u�;��Jm�]F��������������A#Ce�����ܦ��?��Is۾m�̕��v<��m瓝_�����d�+=���OֿZ3��~?�����OЕ�+AW��]	�t%�JЕ�+AW��]	�tէ�Q��,\2���%����%3p��CW���՘g�3��JW�t?JW��Kf�2�!k��-�X��Q\2�y�ż�YC�o�p٦�Q\2�y�ż>�>�>�1�����(.�ż�b�`��!�l���(.�ż�b�`��!�l����(.�ż�b�`��!��pi�t?�Kac^ac^0C֐�[6\�t?�Kf1/��7�!k��-.��Gq�,�e�3dY�e���Z�^(,�e�3dY�e���Z�w�y�ż���RX�>W\����{����Y�̐5d�����k��-,�e�3dY�e���Z�w�y�ż�YC�o�p��V���b^f1������^�6�
�/�.?\~��l?�_~i�]��w@a�Y�f��~ˎs躋U�
�
����߲���`��V��J�[	v+�n%ح��`��V��J�[	v+�n%ح��`��V��J�[	v+�n%ح��`��V��J�[	v+u�CW���U��:tա�]�n%ح�Ua?����t�@Wt�@Wt�@Wt�@Wt����{Da?����t�BW+t�BW+�c�s�p�α��.���'���_aᲃ�.;��ಃ�.;��r��.��r��.'���r���ݭ��n%��Y��f?�����	�=�'����^���/�n��wW��K0C֐�[v��_;bo����l���g�lt��=��{Dd���bp)=_;g�;l��o\�`��7WX̻v��������|�?[��      �      x������ � �      �      x��}ˮ$�q�z���aL+�^Y�$@�!&��6�,,Ȑ��wfUwUVu\�:����|���Ȉȸ�K�?~�����W�g���˦?3Ɣ������������X���ߵ����Ʀ�5���O?�q��?��~��/[ڧ�߿Jm�����޺�1?������?1��;~Z�4���ù�`�ӛ�#�����Kc������������������������!>��h6���� ����1����_��	������uF������3������,H�����$��Ͽy�5|TNBj�I�)r	ٰ%��l˗q�lҮV��������3�v�Gί��?��������?lm|���u����������.$%���@��,wk\���Ӻ��_2}m`0Ɖ���f�qҎT`1q��4N|������~��N�*'������~ʧO��ҝHA����n��%����$%��&FLj^(��#�������$(.`�|��KêXi_�a�s��`�gc%T���_�O�ʤ�4
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
�E`GN>�TQ���$�C3EӺW5��2o�s�a�8�T����8=�L:!mE�$�9@+b�sw���r��X_�r�y���L�3+�*�hI�b��y��[_.x����aj{��8	<�y���ߝ����f�"��aCTB�NQc�8����i-���R�\���"��G��l��qSy      �      x������ � �      �      x�3�0�t�s��4���b���� 2��      �      x������ � �      �      x�ŝˮ�8�e�7�"�k���Mk5rjԳ�j������&J\�n�K;�T�\�\&�[��yد������������/����?�)_�f�����E�^��bC�k�'�e�{���e�������6}��m�6��G�
���~�[�����k۷���3���$l?�����~�����������������h_���i �T �� ���Z���@k�`[��)Z�N:Z�.+Zt^:A���Ƭ?"�oӽr�["f  )����VM��{	Z14�H	� �`v�q Fa���h�>	��"!$h%Ѻ+��^�V���B���K	|���r���	�_����D7�r�	�������W�o_��ٷ�躤h��� �d�� �XO	���r�@н�{�c�f$h����� �b�J� Sq\���X���#����i.��Y� �1��=�������@/H�ey˨��.�+��K��G���+t�ˡ�Q��Cl�k|��̃0���`C��9�n~`�&&(o^9`_��`4Wu�%(�w�5�x)Ai����D�`3b�!D�/4T"�d�����~u�^#L�x3]�'k�>�L"����)A��2��������}�ȊV���͏���]���~3�	%�X������}�*E������Hࣈ ��8~���ğ_����Q`��+k��u����Ε�R���u����+Ȥǉ,�a�ү����%��7?�\=��v/k�l��w7D��0�=���A����y8�4���;ʑ�]; z��M���;��6�/ϵ���?h}��N��:Ј�.��t�?M�����ן��vܹ��ke�}�r
 g�Oǯ��r�QP濟.6UM[���6��z�C��t�/��[��n~����]6�Ü�W����;:B�z�76�G��
^~��U������~=l�8����{k�^����z��=8Y��?ٟ��t�;_v����Ě�)�� �����1�����@���uVC��c
R��m�"�Ο����jW��#���Q2W���~�b|����ٯ�`���p���/������n�;W����/�5�E5~L�J�+�`k�v�1���yܙFC�[�d0�3>�׿��T2�{Cy`�]9��5��� "����?��c���q(̷��r'���h��6������� vf��N6gD�c�l�Tsaa�[�\ �q9��fuԎD��e��W$���w`����y��;;�	~!�]����O�V�����o���>����=��ahy��	}�>���^�>׮��׭[h=�Z'�ri��h�����6�y�`e�5p���qO�9��{ZS`�bY���t�^0N����M������6��C��m��up5޸W>�:��������~;l՛�����kN�k��[o}u��i׿���]�X����!�C���b����}������aԕ����ŉ���r������u߫�n?���pO���y����Ѽ��e����������E���@֥W���}p&6u������x}*�x�\�t���I���}
�P�?l�e�SP�r��r�*��E@������<�7a���Ov������A�j���ۇ �t��I+���	��Mm[s�v�﮹��:k}���	i���}�����<���>m�8s2��r���=�}p>3��k�����a��{?j��p�l�I��i�+��{����}�gw�t>�O��Ig�p�����O��}06���w�k߶���������v��ؙ���~?�:�����9{'��k��1O��-���P^	�ډ���f��' ����~tޘ�!� �k��� 8
��
�ȝ9��oF�7�%=�}قArE]ݐN���/��gB��F+%89o�jX9ç�������jH�B���y���h����M@�5�3n9�@���ֽ��� �1㦿�����o$�Z����q�����W@��1��;_	�_���y�Y�����OV@��0
�߸����\����w~��H�)�����C¹s�4����Ƌ@�c �`$��YЏ�@��kFÔ��2^�g�Nc� �u�` -��o����9�]�I����� @y��\c Z$�S��@z�<a~`�&$pv�K��n�8�� r~�������j!1 ��c[� ���s]� ��%Ì
����"��yٶ8D�*�G �ݯ��`L�l ��_��[@c����ݞ�X��B������WJ�Hq���<p;	�t�Mj���O!�~�0z�th�8pխJ��x�:˹�??��	o�W�*_��g���D�!��v�D5�	�e����R��y2�h�͖���� �FC�׸�� mt���(����_R�
�3�N��	m�:J�2�πÑ�G�����;�7q�p Q���G�m�+y�'��?��l=-��8l.⁃��ʙ_p╺��N,�� ���_�`�
�rB p��c� �2zk� ���)�su��Y��Z���]�T{��y���4���W�ݏ���j{d)d"�<0�g�t �|�CÀ��L� ��:�D p��wP�����Q�(���v{yi��GQPR&�4�k� ˬ����#l�U�?
�}j x`��% �,?5� =�t$���ґH��K���#����ep��D����p��ND@��VJ@��Ws�^:�2Nn%�� ��Hܯ&��<E�])ҽ��L +�x����&Ƨf#P=̧4�	h���\pp^S p^�y �)�.�Б�l����L���Y O}��jb*�!ď��� * ��,@ˈ�G�S}�����	(�y�Z��Kp^ ��4a���!��߮{q����nmߠM �#u8����8�)q��Dgz�7s�̉���.�)��f-%�Z:��P�:���H0�<a�%œ�O�.OYZϺa���L�]� ���fZ{�J�2�AX��k�!A=�O��/�y����� dq̪5V>+>�'֦��??���0������I��k:]�Z8^��`-���/��@7d�HG#%�_kJ�|�k	�Í{��$������-�fiT�qK� �*..� X<G� I�F	@e��@�aZP� z-�M� �ĵܵ��g#m�V�����0�<�J���~�`4J#-�Q����8�?�.�<R���C,�&��"��>��!�7����7n�� F�X�~'�Ύ�.�w̯�[~>2��:�7��7�w�p��ݺHA�]�� �{Y�mi�������T�����CI�������r�e=�x���su�x�z�]����[g�.����WW��[��o�Wo����~5��k�պʍ���[��6�o��;��_��o�պRt�����{_�[��nh}�q�8�)�`�/��'����*��@��������/������Z�l��>3����bW�'�{�l꾟��֗;����o;�@��T�9Z�x����J��}���2�.ʾ��\�vH"��:��8�S��im{u6�~�a��o?�(��կ�hQg�t�n}�S�m��!S�ߐE=7=���i�n%(p�JX !%u! չ- ��>�=@�!%�2�WɃ  �j�
 
Xe/ Px��4� |��j r��vyt_s����Er������������������Q2���O� J��J��C �M���2���mQR��*x�b���{0�ɖ���-�5%u�&x�Ty�����s���	�$&*�.LW}p+ �ᚩ��0ޛ��m_�l�BV �����8��t��`q�@8I�9JB@	�� qMؔ��Tp�\Y�!"Ax�����	�Iƣq!�ح��$�5&�d�w����r��at�W�m�&��*HJQ��V �CȚ$)��ނ����q�s н}��X�X��r� �"S|��E��]�GͰ�Kt�R(��5�� ��}[���8Y��$Ns��t������:�    "<��XTr��\�)�g�C<ø�4~����h���<����%K�g�o^����3�;��|��|�B	��@�y@���j��GWe(������l�#��7��Ԩ'�WG[�!�D��i=*o2W_�nWo��}�qh��~t�sB�ogf��
 p�2��� vN���:���=^S2I(��q��6�}�A��w �5+�� �Ǖ�ح���C��	�e��TS�
�^( y*GE �EO}� (� t�n��ә�3��ۇ�I~f
p�p�����>�`~�S��E��>d���gL�#���
X ,hu�v ��%; �v���kj^�1��M; ;�f%�`?
'�Ӭd�Ӭd7��p"8�J� �
�3���4+���4+���4+ٽ�v�Dp��LB@Yɮl�{	(+�Czp@ �R��Ĥ���Cz�l�PZ�J�&! M��O�&A�  ��N` �<���;|�@b *l��}��Lt��ur@@��)��������r�=��M� �Ыq
 ���P�҇�"��9>$� �Y:[5,O}���r�m�wP<�W�٠rW��8���Iڵ�C4�����m���'��>���%�\�!ތ@�!@r��X`�6�>ޕ PD�cc��S0H������^:V
��O�Ox�6�'���Jc}« �P�%h�ы{�<���u��������&�Z]�Oz%�.և��V��ޛ��[e��|	�1?�y`���L��򐥰?� @�C�� ���q� �.�Ǿ �?Y"y����^ ���t�����y%NoS^�By�/����<hw�>��"
��X��C�_7w�GMv�A��E�p��痵�C�n(�ܩbYk�2��>X�C�B��^�@!��
a[ս��9�fH7����/�SyM7� |Q�o���I�^����޼݅�/w����� \D���*������/�0*��Aiz'� ���,�1c������\�ѝ�9����P�����Ej��Qg[WP��rA��#�8�U&�g���A
�X%}AP0G~��j T�Dp.��ג��ZL��v�7߉L�G���4���� �~��� Q	@���! �G����t��U
|&��s�9A7�xf�I��j�
 k�5�5�R���b�S��WѼ]~ϝ@n����d���'�\�F7�vh�Ȏ*�@e(�\ �woKut>!��Ή}��|s]�B �4��_{�1O5R?��;~�sݼ��m����M�(t��=h��9��W ����~�c�N�+��B!B2�:�CA �)���VH�_Gs((1�����_:1A���н�$���^�����渗�,����hhb�:|�� 41>�L ��Re$p��I: ~�q+NFH�_s @/o A,�. 7���#8�" �RIYGЊ�}�4a�V�H��|+�u � �U�:�C �*a�! h��^����֑$
p�sO)�g^Ծp̋�q@~yO�E&h��<f� �r�K�{	�_4�߬5+�;o:�,��~�Դ7�`_��_DȤ_R{~ˈ�7��G��;^�V
�����.��R+�z�v{�˝�ZV���͸ci��>�n!��*y�q������rǖ�X�m�P��ڷ�/� ���C
 J� px���k  �Z��^ <8�6�Wy�IN0�IV0�I^0%P/������Dy����*��A[D@���jte����\����
h@������C���؍�Gt����^�O���t�=���[��ԫ�}����Yǎ]��ї;���
����q��Gz]gL @��_����G��d&H ��;�Z�KB ��ki�#��Ը_�wb� �m�#��^�c&o xY��7�up����C�Iݚ"��W��T�WTX*�PW�lv]1�	LЍ[�X��n�N	6�!�0�������K O��Ε��<��<u9]?=��g7~v�g]�c61��&u������ub��"�RO�����t������P{��E��#���RO��,%8��5l�ڽ
����;)BH��+�����P�'��h,v���P��lJ�� �'"�n������X ��cZc ��Zj� `Ղ��˽&�*��W�$`.���$�w������o�r�ȁ�������f.�Ze�߄pn�HT	�u�bts����[8�BH�S	��Sk��4WNN��^�[��@q�e�Ӿ��r'���yj�=�4?�:�hUݷH��w�/wb��O[����0*֠QP��(����oO筓
eY�������u�o��������i됾���ۦ�u��ҳ���o�|.�r�ׂ���xg�O��Y�E���[�-Tv�����ˍl��x��;���L�w�Zoj�*hY�Qne�S�/k��fDY�T2C6�\�7ux�ӭ��D��t�T&C���ix�u*��d�Si�|�4�uÍ�[n�.=���SI�G��������A��#�s��y1���?���|��0�o������x���2ϷE0�����'��G�>�S��Xc:a�H�����밍�ۇ=n���uB�	p�����+(��d��տ��B�u����G���+��W��r��!�]&�"�Ɲ.����%8�@��Ѿ��K;�^
о��pe�K jx��7	<P��C�� ,�P  �
deP�@�Z��
 �O}�*�?�� bX�] ZX������ ;df�"*%��R�+�M�J�mr@G���������+s/P޽O���K��4=��tm��&�0Drv�3
�I� �U�d�#������A~[�=P%��^�r"}H�v���%A�|�u/�0��0��k���l� �U2̂��	�;k.�A�_BAa�L�ի8w1�#T5��������"��g�-�*�"��a�z`�G!��#a�rg����:d G�5�x6�ɢ�􌟟m�6�M70��E�����%���MoTo�^�E/ϓ�H:莉�YB-D��H��b}2�V(ý��ڷ��bQ*!�`,i)eh�r�i)eh�p����l��-�Q�!� �{״WP�߻��" ��<渕�"
�� ��w�^��������+���Jp��p��3���^^�v���թϽ'�I~� ~�45�P�8-���5ⷵ�_���X]����p�ѷz�0��IL�����<?�\��95�����T�>��؃T�Wܢ;.���ƿb��55	[en��tn��tn��@�j�[q6m�έ8�(��ӟnrU�܊3�g��/s+Δ�_�V�)_�̭8S�~�[q�<�B��LY��nřr�݊3e藺ud��/u�Ȕ�_�Z�)?�е(Sv~�ku�����A�B������ՙ2�]�3%��Vg�˯s�Δ�_�Z�1+�P|�J��9���S��2�+]�3��W�Uf��/u�̔�_�V�)���=S6~�[g�l�Z��Z%��u��J�2� �V
�Q9P6�^w��Dz���ҵ7�B�ko �J��@��.9�*]�s!T��B(=��Pu�A���9�
J�sT�v�ت���;�V��09�:��FɱUB�7JN�j}�rj�P뙕S����˜Z=T_��ʡ�"C����l2}�5J�?�j�.�{z>;H�V��������S��tØ7�m�+Ep�,@)@�.we���6��*����������?.�w�5�����r�.�����1��; ��r&ͻ�a(O�3���-�W��/~�f�����葪����<:�5��j+'��a|cD�u���1����?�����10�}�����o��@�kHi~k��5d���s:!���A�;k)���!������X�e xl ��ު>�� RT:`�-ʨKt*(ہ� �z��
��|"Jx�I	 "�h{�$PMD �P��;(�`��)зk?�W)��    u}��ۧ��Y�~oF�퇫y������b�+]}�nl�2
������?l�$�����/�z���P����# �~U^��g��t�$ߋ��t� �t��H ��Fe@����nkB�4�a8K�襳�
�.�!�&tr0��x�t�_͊�!��Mƕ"�5���ic��Ȑ�>�l�hd@��/�>Jڜ/�BJڔ/�FJJ�����\u�vNt?b�C����A�k��:)�hӽ�:)�hO5j�A��_͌�! 7VU��(e^hR��w�oװ�;T�-��tPO$��B�����n2V������ 7d?�x�Z�܎��Rܿ���B~�~z��� x� �a�����|��۱�۱0=r1�RnrՍcʋT��[:�KZ��I	ʰ�Ȇ_��2Rʂ�}�(�a�_H �-���'N�G�����b�pޒ��2V��䀹#S�玜�0@4C6�����!�0��A ��o�U�j*( Zk��	 �d�vz撡���db k1�!`�q���
:���mL���&��J��ƾ����GB���8�� 	֘�@G %H��!�
�l]Bf�����W��T�!�*����Zb]İ�Ǳ���
G��Ӿ��r���mx�f�o�!�TAB��K	ڢ�*H���*H���R�#�mZ��G!�����d(-�*
�]�Ȅ`��2=�޷'�| �;� @xG�
(IR?>!A��.��AG�.�˸�#���|����͟�s�88�{L'�A]����n��h,c��Nb�9Y|q����hX��w�%�FC���椂 ���jNJ��t5'%�˘�z���܏ ��Gڇt�c�J�9)bhW�՜1PV��ZpiЅ�u �\��R��N��ޘU�×��C�� �q����jZ����B (FR� ;�I��;|y�S:�d^J �.S��� vW-*����o˝�����{:]�Si���ة�H���TZ� hm�J� d"�g x�v�|�4�C���/߆� ��{\ 8 �w\ ��wO��v?��ip�n'�=> ��A���7�>��{�:"L�V�d	A���7YB��&>��m�]"(���zᔠ��zᔀ�x�+�<l��7T�P��N�H��8��Ak"��tFK N9c
8�ɹu�X־Z-k�,�s���e���~Ѓ�U�E��aw:8zN^J �+c~�3�u�����H �	WU�%� ������^�|=b����j�T��A���.k ?�*��6CM���6IM%��6OM���`R��}�m�+c����t5�<�ڔ5�@� W�*�+@�-�Q���[�~rh���]Sz}�� CB���r/��˘�Z�@f��-�3���<E����%Bp/���]���"P��˱p+�����AA��ڎe�OP"���u�ǝ����	��_���ԅꝨ�B�<����z�-X�DZ���:,�Pm�|	�%)�/PE>_��8|��֌��(�"�,tR$[��6H�@=q�l��(O�%�~��*����XE�W�0��a�� ���(�S X`��{
@��z�,���
THQGA�����,T#EY�F�46�@�mld�)���5RԱ�ꤨ?
�JQ~��3�@�5Sԡ'%��4�@m0VIp8#�*PHE��Ur��Z���T�.]%ñ��ͺP9��uɭ&JݬK&I�v��ͺ@E��u��?j���EU* �+��P@5A��}7���q��������=�Ye����������ɤB�?�� ���׸��m��u� ������� �� ��o:r��]:rAa?n*�aK��4TZ�l۟�uyvj�%e����۹W�,c������urf��֡xAUUu�ߌ}o�c�SUv�c-G7������T��� �����P� tR��,�T���N;Z��VPf1-�Վ�V�ӎēW"��W��F$ -���Z�q�
=؆Y<��A�JQ� ��Od� �(�FZ�/�GPǢ�JZ�/��o*P��Ќ��L5B���)s�Bm�"�����Uf~hR!@JZ'T��!{�jayqG�{��c}��Zu��/��^|�v<B u���|�h��_>�9�j�ɩ���Z!�F��.���N�8~�s��j��T�[	�B����V��z9o%8���!���O�Ի	��I�Tl5^�ҽ��X
G�.Z#�0ڧD��UF�Ԝ<Bh��^�xދ�j��^A܋Њ��^A܊Zu��+�{ ��JSh��S�Y�Ъ��^A܋ ;�F;#�8����#�q�⎀�(���X�o�ӊ���'�ϯd8R7���u���νc78i��+9(��	E�� �'�y��"�`�q	ET!��cw�H⎀|)j �D�z��3�9E�g�ca�ҩK��~�*f
8�qr�"�f�� ��J"��#���,��	o�+0���0��������'C��&����6�w���0��l믆�2{'u_@r�껡a�J���=l"���i+����*
��vz
PKw5;� ��W:q?�A.)��1%�C|
{���N�ޠf�ogZ�f� P��A� �^���\~#��3u?���JI�x ��9���_�{��s~�7ߩ��k�0������o����@y�O�ULR���e��ᘧG.␓�h4B��'��'�v(�HV7ʃጊ�ɨ8�9`H�*�Ȓ���0�+��'���'��xiBzj80DҌ�8���1>5 ()��"�"i��[�ZaNpyE�[qO1	�آ���c�]-:9E� �?�#Śe��p�#ѝB�!]Si����G�V�����d<O�?�1IJ�.���]tןA�ி
��Z},��d��8 �(��?D����)�D��������[����[NN.AK��?��h97�mrS�}n����ĸ�c�f�d�Q!�aΒB� �䒪�n��suF G�%Y�
�5&?�jTp��=4#��a2��U��/ʰ�`�D�|rG���{.�0ȣK�Of�˝T��!k�I�pRDA�@a6K],�C3�z[� ����1�Bf�x�=ɥ��1�
��}���&�:�X�Z��<�9M�!�n�t�?wiz�"�#� �ZK������YW"!��튻���):� �]u�B����w~n\Q�qw%�B��`4������pܘ����8�忩Q���H��)ӑ�o��Yk~�|Lk>���9�_fz)uk�~�0����gC@��6����V:���jp����=t��Qht��R�v^��j��M_"�Bl ����f��1�}�ǿ�<�
������Q3?�\Wo�M��m��19c�!j�'�2����Gwm��_�@�!g,@�i�C<��qp2"14�CV�!���;nRB���!�f�416�c�]a>ҰV�B6�2m�}�C䡹1YDw�����(���N-�1yJ?[Y_��u/���a���3ԏi�}n�M��@������@����C�B-�`+�����-Ԕ�/֠���W�Be�����2��!!T��{�[�/�wk�PcFqe�ƌ:��B��`�*��[�PmF��d�ڌ:��B�u����3b�/(9#v���3�@eg�ngTwF��F�g�[j�U��X*��[�]+����w�*�?��ݵZ�l}w�^��C�V/��]��b���R�m;PKq̶�@-����R|\@,����R}$TZ�Ԧ���UImz[[Z�Ԧ���UImz[[Z�Ԧ����Hmz[[Z��V�δ�(ΰ�L���jb=B+��rb��E�Q��<��6ͱ3���r1΀,j��8�(K�<#�0j�=;�*�8ٳ��2
Ow�mUQ�kzFhUQ�kzFhUQ�kzF������pέ�5�l���\�ζ�(�5�l���\���ٶ6״s�2j�~�p�-Kw=#�����h� '��ڣ=gK���p�-�;>#�A��mnΰ��W{�V�X�k�Q\��yp��*q�@���eԖ�u�Q[��yPFmZ�A�eh�    eԖ�u�Q���A��
�Q[��PGmQ@��p] u�V�u�Q[	�p��V�u|{��p] מ� �ZuW�u���p] �Gm%\��Q[	�E�|�V�u��p]�Gm%\�5�J�bkt�fp���u��i������1V��|��~}�4����/w*kǟ��A�{uG=І~v�DT�Ɗ(��=��.e.�����AN��\@�}��'k���]V�"Hf�i�U7|H�tײ��d�"��ď�,"�҆^:s��!KG"��؉"�;��(;�q(=&86T?$;��/�1�(Aə�7��y0E�~��C���ך�y�~-l���y0	�n�C����,B�|0�J=�m�4=�\] ��F�+��}T�v�:X#O+��s���dŚ��lڎ�厏���1U�rPL�ĭB���KĮ��|�UU��v�}��3:,&�p�R͈�t%�<ݮ�;�8����
�\�!A��jFH�|%���I4���Lʓ`@�7���T��u\26�!�;�Saȃ���To`��T�T!�r͋��vZ*d#l�*��B�E 
;Q@ʬ�p�� �)0�9@C0do󛀃��xP0�ɧW�p(ތd؍��D�9F|��J�I�4$�rɧ`���e_�s��a8�yh.�,���R�TD �B�{�a�9g�CJ|ΐ�nɧT��l��=^���"��G�|=���־8��C�E5 U �똼���<^����
�~��+�nA�E54eP�d�c���.ut8��t��j�w6e>��f�P�$-i��@�����
�S�q�)D �u,��S5 ���7k�s��+ش}QF���k�[���[��2���Q������=�1�g�|'ZG�
˿Ҿ��L [`�� v��(% iL��Kvx�����h�>��B�c�.o�;���3�!T�p�8�w��3t-C1b���>��ҫ��v�C%�8���^ЦѸ\�Q��j�&����jz����EtrQ�"=�GW�t|*��:U,�IsvwV�܉��`��;*d�9�-x�-[UP�c�تB�
K�VT��c*z|*����"��z�==p�P޽\lOCa�O�V�=Y�ih��!����!A�!�.I`e�ʒ� �+VU����*JZVj��*�s*yT�t@P����9�P&�{�̂@�<VI� �*N�kT�xBp�>g�"طsm��wLɥ#�ܬ%��n�ӱ� J���Y�YI ~��:G��6�� `����!�m|u� ��S!��E�� �Q]3�A�R<E�@��� M�;���JO���SR3�@-I�e��U�I��ZJ�3Tf���%�� �d�@�Z�c��Ӕs�����9`������5@��Z�%�u�Rj	��WB-A���J�%�YRɴ�b�c��b����������I��c�U�צ/\�]x;1�3����؜M �r'Oe�bH��9�h	(d೸.ْcц N�����5r�W�k�cV3ж_3�Ɵ�=�B��l���*�� y�+�f ��1�O�� ��+�` �d�֜DK{���s#��,nH�c���'�!��T>���� |���u��w5�E�4$V� A�U�W	��F�����
�E���JhR;���+*A�p�"�)VG�H����V�t��V�t��V�\���ZiBwdmG�?�'K�
�UǺ�����or����RC�a����=�H��>JZu4��x/B���Z�E�tMA� eSGH۔.M�{ uӵ�v+B��M׆۽�:���$��U����^�g�V�s�t��U=#D��u�5D���چ���ɺڙ�UJwmG�q�Y��u�3���c��#Xm�ǌ�#XoG�����S4R�|v��O�w�]:�J����������m�+��kn�P�|�'D�]-wTT�\�&��yT�ԞqW�:_�=**u.P��g�;�j(�+(y�.~D"]�ŏ
J�;uw�V1�ŏ
�U�j�h�Z��  Ш3��Y-~TP�ҩͻ�*f�����q��f(3t�>���\V+ؗ��mV_qj{��1Ӑ���7��˯��5���w3P��+������^��� �Y=&A'���n��{�⹙�p���n��ڿ"*�+���I
Ĺܙ�����?�\#(C�L�� (�cJy���c2uu���v2D4P	�Ҩ���*�~%�h@����h@�н]�r� ��*���*p��� a�KEu�g�g�'�1�?�|=-�Z�0B4o_^� �9Y�	(d5#�S�	���!AhM�zFHZK�c��!4*ҡk�~�[��I���Wd�Z���2��n�T,]��mog�C��q�!(�SեG<�iZ�}��r'�og_G��Η�."ʙ+G�9����ź=�t'1B���e�`��{��T!�in�p?Hs��G�|=�H������+D���;C�9S=w�����V�SFW%��(@&��{�֝�����Ƭ�f?$�D��=����m��4E��y��(oc_9`�xH^�L$����\��]��R��U���ͬƦ�їd��mvd�y�����N�Z�6�J�?(��H	 �{IF,"�LK*`A�4��8�X�ۗ���;vH��>�p�� A�LjY� ��j(H��|�'7HSJT�#tiW�g������{��b��lnw3���� ��M� ex�/N�M��{}1:8�9�-�r���v�����7Y�IC+�����a�8#��dx�+H$���V$ݔV�L�a�\o�4�L����B�����xZ@�2���qW�w�>+B*v���uǐ�Z݁TS%���;��R��GJ������>,'��x�<Ue��Ŗ�lX�����o��-���د䆶����c��%Ul�J�,Wn;�w��2�w���Ǣ�q%a8^����Bw_Bg��W��p����n� �-^����t�<8�ī1���� 8"q��Aϫ�.���+f8")�\"]*�dH~�ݕ��z8B�Kռ`>dp��-}��7�]c @����k9�x�|��LZI}m�'��h�a��FМ�AM����=��m��P�H�������A��a	����S����a�c�qhͷ�X?�0�+5q43l�-�*�永ɫ@��Ϣ
N��#N2(�����R������U��ǲʫN2(#�7=b�� �a57>���XNr(�  �b����! ��R���aY���'8\J�=$�GJ�Lbz��YN�	�͋�����6�y��ʼ��*��B� nѣ������^����w���R��^��!����r� ��F�ִ���
Ng/}��;�*F-��3��;��G����8ҫ�!m�)��3B� ���P PЈ�#(h$j fd�������YC����NB[�>�|��;3������=K�9m��+a�߳��187���T� k�uE� �}��BF��~��i�q� �MPt�N�U�D	X�k�=���i�P�w�_Ea
2!砍����`oĸ��Nf�8lĶK��;��\b6d`D�{���1���� j�� �a���/|}.��e|&����m¦ [M9�Q�� k2<�.����jh�0`�]�N��`m;��t	�n\��܉��:M��L@���7%(�2VVP"�`��ƅ��F� r��Ɯ)���N��;v(���I���d�+	 `��G#% �1��7N��/����.����v�L-R��!�b�P?!@��u(�K�x��f|$.W��7�e��#p�^�����!��S�N�#���=5.��uHa�g��9��'��9�o)��M9��W�a�p�pc��陴\c{��c�w���Q��wz!�^�q����B#Û�v�6�!? �  Ĺ���('��*�v���'BH��a�"�c���B�7��"@��p5�I�4}���I��x_L;�	�&��:�V%��`M�M9evz`�2��0�n�oA�@���
a��#��}�LPΥ�GDkZV
)��r.�F�q �Rɤ���W��Q�b�F˲7e+��:4����r�3����إ6A��'N^�p���v����O��
�$s�
�$q�
�4��zN)�&ͷW���B��e�.��)!Ah����� ���zFH`��Z�,�P�A����fe6�������;?T.'(�I�)cqh�{�l�BN���#���}P���֘B{�)N���zH���.o�ƣ
LH�ܛ8p���-ը�Q��䃣��rʰ�O��<1�re����Y�v%\�-�a���M�B&TWSlL�~`��C�C�D����֠b����F��s�ai������JkO����"P�ѥӧ[��v�r~d
EY)~��Q!>��8oZw��j#$S��77����$�����1`ݷ�W9�9�1���s�'9���^s�E3�~$5�0S�J�K9og��H����up��B�KPÎ�ƽX�!����!�A߼��`^�N���9� @���6�v�֒|� ��g؈��H ��8լ2I�Ŗw�aT�5�'/��E`-���"�7��6��AAj�@>�oT쨊�j@�V�=%��v�oձΞ(A8Is������K�ǚg����->�?t���;q7$���u7D����8:�V'?it`GF9D���ڌ�"@�Ssi���꥽�p��*��rH�U���vX��b1L�ώV1����nPL{iS��*����.��SA1��a�]�S4B�ZUc�G�Ғ�>K�|����c37R��E�xU��yC��ĝ{�r�JF�sڠLbH�ZדM���}��ǫM��N�;�+g�����z�0߮�;����93�PZ-�^�����d��Ö0o��{yX�T'Y?GhE�1B+�u�l	mS��x�*�'A sr:T�!@.ֺ�T�/����H����PPt�Z���c���'�����6d�e�������{X�U����������n|��ÿ����?�ݯ%�m�m��Mi��|n?�um#��,�P0Qm�w��r�^p7�!�P��\�����d�>��1D��n� ���_��������      �      x������ � �      �      x������ � �      �   {  x�m��n�0E��W���E#��ҘG(#�*e�+Mې�F���5i�$��{���3cY`�bڬ �OQg�a�`!�	Qu(c݁�3;n&�..��䊬�kS�%cQ�rY�N�C��P3��\�<Q�&Y
C��<����6<�Z���{k{����,v۶�Ʌ��=�FhSg���sXb�U}���)l�V9�/1����qϻc�iFm��Ď<6Ą�I�C�Ww�F�:�8ߘq���,��9�M����I�\$��:?��D?�`	���֎"����R/"���䔖�n�MT�ri����n�Yt,���E��t��2�2ɔ;6��ӦP�iØ���Ԟ���C����[�zb�/�3��      �      x������ � �      �      x������ � �      �      x�Խ]�k�q%����>("32#?Fc0ڃ���ϒdI%�$�ү���Mr����ӧ��e[���|���+�J��U�����z��x�\v)�[)5clNri���sӨ-��=t�BMJ�����O��/�r�ݏ����J}��_
�ş���Q�t���7��ۇN#�0����Y�x�����CP?h�M�oڟ��>����/>����������f�&}���x��{�I��d����9x�l1(���i8�h^���6��(�Q���f�7���M���`�a8�~q����\t��dL����U���m��~���G�~���:����堕������7�خ�'7���f����F�j��'Io�7�&�����o�&����7;��Y��twɁ9:��c��C�o��ν8dC7;薁��6(�=�O!��S
=F���rn��e�N���>w2���i)~�����C�oʜ��gGg�
ߌ�f����9��Q�`^Z}���1��D"	Fgg�^�����	�Aw���Xmұ��:���ǂ��TɁBu�f��6����Rch�Bov����,�:;�}��r/����8��������r�g�֡`U�ؒ�SB/	�݂��S�9�����vZ<�5<�݋&/�ه٧���ܶ��p��p���r�]_:��͆���{���iw��,F|��$���t��μ%�MQ6)�R�[]LB���nl�ʪ�@ѩ�jN=F�x��2�c�zc ��כ>q�֜�_�6�v����Z�E�Wߵyq����䎃j�v=����)ZeB*�����Y�u���X`Zq5A�'�e��jE^ �n}H�y�-��M��約�%%1\~m�t��@�����㫦��,�k���aӛA��r)��B�ՠ�UW�:t}�.Em�M�z�Պ�dq
�����|Ї?�I#p�ŋ/����5r��ه���]�}��9Pח��>�P3��31E��P07�,>�w��	*iW��[�������+�HK��|�5&���Tl(�^p���7���~e5y�
�|�l�;�=�V E��J'����lE߂oY��ѹQ�,e�'C��SC��v�t9@Y��yp/�~eϡ�b��S�x;l�%���\��"Z+�OQ�8m�޸������6\)S)��ګ]�N7��S�6)8�+�t<e�/�>xV�wkiP��������ν���"�ñ�/G���qkŶf�ɱ�rR�x�
J%�d]0=e��ͥ��)��\w�Z���w�\N��Wn�=f�� z.���t�?g��F��,Z�9������F:�&GN�"�J�pP�Eˆ��驓w
�hr�.�ںV-�HN޶7.�[FS�,y�]�k�t�&�䨁����`���u�b$��w�Xg�Md���X� 9qH�#��[������>8�[��î���<7�$l���v)��j8%�����Q1�C-��%_y�Ψ�O�T&[�|t_�Τz��J�����<��:*Rnuv���f4�)o�X}k�=�k��Ŭ#�G�l�EOiV�8S���Ք��j3����Hɩ���֔���Ζ
0Cih8e�ϱQ,��Q"���~��)��λ�(p'�J���kk��'|֊����v\���IÏ���û���x)~��@M�}}ƽ���O���o��?���������	���oկ�o��o����Ny
]��Y�Z��Rɛ�r8�o:��kϙ���S��3����kt�,�S��yI(T�dJ�ܻ��hi �%2�M��"~+��m��Dm��p��E(3��:���A"e$�hq�2�b��:������K��?X[~���/����~��_�/����s�Q�4�⽣���g���fL��`���o����R	�:p����ܔw�1:y-�o�}�������mBc��Y@��?��#:���\�*�d9չ6�9Q=F69��k�������J6�eJ|,��[�q�V�צ�kxtsMW��"�Ht����"��-�Ћ+�.����9�f�R���)K�樤��~Y*�Kk:WD�rK�j�ϙ�p��؅k��:�ǻ.9Ox�ʏ}�1R4�g�n���1����Qs��Y�(?���,ms�u��U�v�RL�]ͭ������,{�TU*5\wX�d評���R~�9,J��׮F՜	'Ӓ2.��R�Z��ʱ��|C�#�W_ �J��J�/�Pn����f�����6@ǚK�l�V]�Q�>���O�˒�붒��^c��Z�����j��n(9u)dhT`*G٬w�;KO<ߴE�Y�!��a�ef�9A��!���]t���}��nE_����h<�S�-.Ō]� ��]W�KJ�|�^�Px:�}F�)����Dǫ�d���f�v�5Ee4�9��9�B�I��Lӈ�����G�zfv��b�v]��ٞ��B��޺X*Y��N���Nt�!�91E.T����X;p���8�����1>EJ���T���*e1o7N�����t;l�ǩ���3̚,zKo�:L[�J(�Ί~�N�,8��۸L��i�y�:��R�"]*�@y_K4U�b�;#��l�-p��wMş���rP��?�h�ݬ��T���������o��Rk���~�׿���EP�g�M��:D���)�$�i4}CN�S�a�/=�\�/-�WP�s�F_���6��,��7�r��z������E��ugu���i�]�n�c�
��%Xv��L�aj1�B��"�.�yE	&]\�0Wc��:��Ȗf-6M�(��B�v=EE�x�Z�3@<Ug�����lgZ�s\nܝ��q)���h*����[#��hͦ�T)�!-z����$�J���s��A8��R�HY���uO!y�7uo�㜱�Y��h�Y"�O�Ps��J;�!�D�3p�H���aÜ�:�����읛�Xg��s �����u���((�?�h�bJ�d�#�s��{��;��c:l�2�� �����XI��oX�xл����^��P�m���+����ή%�p�8�V=��*��z2��b�b��U+}�gk��C\��R5JYȥ����[�f�a��2{v�z�T���B���C'��'ܚ����βS}AW�:g�O�iʧ��^5J�ZQ�tM�'"y���)�=i:g:�`�ɓf�.)J��������+.D�F��J�oW`[1J�7,Ƽ���_w���C�f��u��1Y��fS��K2��6t��U�Kv`�f))������s{�(K�1
��+��D�r*�|:U�l�A�E)o��%����G���� |�A�Y�����N�@4��� ��ݾj�ᛄZ��˨T��R���[x�����RX�c�z38�i�AkC�����3n٧edҮǭ]���>�|6�Uc|F�؋M��2�ř�.��W�j��U4���M�e�a��u�m���J��f��"�<m*(�^[O��
�
���\d���O�#C�N�*i��;:��_���d�Ֆ�D��ٺ`r�^?W��Ҷ��O���h y�)|{�״���և��x�`�7�A��n�so��.���Ƞ�*��O�R��<��+�s�n�����6�pZ?���v��C�8����c��}�`���a��E��Ne�����K�OQ��O3��Gh8�vk����#[�Q��6�\_?�W�
��Z��@EJ[@-r�pq_x6�t��^k���=i}��DL�����S���M�������G< k��:2{�#���R��y�GkD�]V���i�����m.��x������n��<��8hQܖ�]^*I>줢-ч\U��(�[�5�v�J�XO���+Ld�H���Ù�/�
}k��L_��RѱDtH��������5��y��nw�t���R9�]��.��Z�'��բV9e��8��������#zG�Zm	�ӝ3\�]��{c0>�:L{,~��+�����r����D��k��Xc�]��ɵ���f�ev��H�5W�K��Џ��=��	���:�\�]�b�]���l�IRWrdj������td�.�M��j�M�m�#��E��� ,�cQ5�e��=�_�gs��L�bo�U���b��N�y�+�R:���J    ���F��_��-F�9�N�(eyN�1�������Y��ր����r��i'=͸��;w��ϊS��c�;�J~��,�X}t\�S��%��:O!6�lN��/r0��ɡ�H9I��#���X�BDS�n�F�֩�}@�=Fe����0�~�A�ʚ�����ģ;����=`�Ke��*�/��Ž�3
�˚e�j[uS���_����7?��'������_�����_�O?����/��>���/��c_,�;����_d�]�hT��n�}��Ƿ������7=ڧ�t�j�g���EV�{]9��}����E.c���3�Ad����t�$����4��}k0;���맺�*m��F�پGNڙ�迩�Ү05%R��
���M�����W�j��gܖ���7�Bx������0k�݂���B �"{
t1�1����[ώB�=�
5[����!t����=ۮL��TC�䬎��:�c��%+�����n!P�������s��6w����rQ.7y�7���?b,�ݿ�?�����:#n)*�9}�;D�m���J���5�b��ԩv��A��nM�K6���J��
�T �a�_�o��8n~�DVj'i�~��#�a���,�n��k00S�_�?�qF�vK>�)��0���Z���, J���9G5����kj�?1DY{�9���U�3�T��ww
�A��V��0��е�|�[��x!1ҟ�CѮ�������Z�o ^�g�{M4�pb�eꪅ�f��A>�9�V����E��8����O
Z���r:��k��ɚ�C��j�Z�"9~N,��F:
T�r�^�YQ��9Ll������)F;ն�q0i�z0�e��Vw[L�z(��ëU�Ғ)L�B����w�^
98�Qa����2FA�5�2��?.�s�>I���rL�ջÃ���?�f;!!os������*�Dys�Ԇ��,>:6��}+0�X=Nfu#'���b"+&�SN��e�-�nq�R�4��ۇ�h)�l�����}�ւqg\	]�����z��s&D�r1o�Y�ϐ��+�Ee�$õ�և/�����P�x�詯Ϗy���.�z������T�7R�6�f�}�<����d���R��R`��V��`�^�f?�(@1J.yi�1>�6��ەW��F1�+c����.���}��9�ȸ����eexiWS:�ﳵ���8��������1oį�7�SѪ�+خm�y��Ԣ99G�Mԭ������f����
���u��Sz]�'縛L�J�[��7D��8�
+�#�Z��ߔ��P�`�wf�ߵ
,_q��30/
+���Y�7w�J�˸W6�h�>w�rE�X�Ʀ3��bV���&���g���!��"?���਌��܂7��_�oa���B����n��\b�svuN3�=�`��nLY�N5R�߸+o�)/�%�;�����nm��:����$��J��?��d��J�t��6�u���4�J5��d?c��̕⴩�2T�<�é�޻/֗��7$'Qsw�A~���iT��:�j6}�7�����Hv��!!�����d�=��VB��|������&ՔZ�͌Ј��y���������F\�Z��K�߯�����Я->v�a�h1�������	x���rc���Ւ����B�ĵ�)	���S�����4��uKN�}�zL�IY����N�f��F1��)���*�{���~b�2��q��\g0{�����	�d�aZ���t��F���#��fr֪�"D�i�8��Ke��
���(3Lsv�Q� ����~�aa�B|~�ң'�|y��NB�~69h=�~�8r��{x��8^�iI��K�=C�N���־�^Lu�3z7�I����&ކW�x�QsT7��N{��S���TɮM$�1x������ w��+�����eaN�A�H����~�������G������n0n;�~��Cu?�$�^v,®��]��6v�B,�苡��k�&�!PA�X�U�����t��=c���X��H��L/����ecVL�5p�q~�j��A��)�n-�:��R�X�����]�SR^���cҨ��̩������a�ݑs��q��k���T+�4?wY_s���~�����8�D2y1ġ����k)l�V��ј�:*&��f :K�P���TR���Ŭ�t�-Z��hmR��!*\ �S�x�7�~�}^�J����OF���*�햕5n8
� �3u$��V~��v��p
���\P!fJ�Йyy�@(�#�
o-�(�n��]����o����=�^/�vl���1�m\�~w�Z�'�ϸ���P�z���T#���2�A�Ϋ}B=��3�[LAT<�d���Qw�m�V)4wM�0�BKS|��X뺋��%;z��c�|����u`��A9IA�C��q��AԤ�:E]�����':��̌�ƃJ��37���] �C	,��JWɟ��%�����jJ]A�P odR	��(�����՞o�+���H�h4g\S���E�^�K�}E����j}Od9R:�-����S26Xg�@2�n��Tc�q��l=_!���\;c�۹m�E>k�+Af�<����*��*_���;f��82��Na\[J`|U)���N�k���_PŜ�1f���C�L��6���>��ts~��:ݏ���q��v[�Wk�mdXi��,D��T��#�#=\����ϋX0�)P�����4�b|�\�a�+���+V��a�b�ZJ��9�);k���7��pbZ&EI��l�HE	�c%�����)��a��j��J�v��e����R�P,Qš.�2D�?kf�U�)�Ɩ��y����H)�O�OF��#;#�q��ȱ�fNt��@.��W���[C�H�1�I$��R��T�ມ*&��c�o~�(�����vB� 8dJ��Y�g����>>1��w��k�oZ6�_G-�Tڜ2����f*����f�#�8����?�a�u�� �{ﱂnݑ�:a�������C�! ~������5����8cxQʶ�36p %y~w�щ� 8v0%����Z�L>(�ZRffz�:�rS�H%ehe�����Ѻ7��?��!w�o�6�P&����aQ\i�����2HV2������t�.i>ic��G�S�Ō����?�)˫�T$OB�HmO���e�j*8)�
	��dPw[��.씐$�bȄ@7�`β�'�$��mNJ(�Q�[,�c�����Bc,PA���y���n�n_��4:�}/���`������Qmj��HT::������dmj�pD+��F�v(��D���w�,���Τk/���a��K�C�U9���b�\�Y���V�x��JV��B��ū�v��+���+ )5n}L;���i�Xޗ�:2S��v߃ѷ:��5�%�_<��&��4��d*���'�}�.��>�:[�ɼ1�|�k���&�; �c��0�L�#���U��Ņ_���s���������/��������O���}��
��an{�r��Qͩ<�岺��T�����@��:�vU��7r�~de2%��7�Y-�� 滗��;��n��;�fY�Ay�'�1&BYX�+=�~�2hj@��#��P��L�=�H���Țt�[QTfC-X��.+�p
��#�f�RH*��gt��a6xGod0�;�8i�Bپ q���m|������:���{bhW�]��nHK�l��j,��
��a�ʙ�U��6�N�.��d���z��oP^��ڎ�V�'�)ņ�klE�{h��Ż-=6(��n�B�鿩a�t��L��q�V{ÏNW�L�����}�}�h�ྶ&��S���_�_������?�����ݟ/_�EKNɳ�8�zm�r
9S|s�N�TG�թ��5�']sU��KF�}��79����p�������sm)���
b���C����/��.�K���P7�'�$���Es�|4�a5Fn<г��֚��
�9d�m���    �s9��K�Hy���)3#]j��}�}D� ��(�\2�!	c���v�΂�L�g��C��h�Z�9O/�������M�#�?�S��+��}e�U��\���*�b+�Zl�싲����w>�Z����6���	�;|s��ܛ� ��1W��Ċ��������������f\宨�s٢�Ņ��GRq9�!)N2�=o# D��)�TB�0�|�B�xӲ@~��*��=�����!�k�x���Q�/����s���}�7���?��/�Iտ߾sj.QVғ�T������6zߥ�N��?�:SЊ&Pt��RO�
mFE72R暬��\!Q�5٦�·.9�~o:7�l�r ;��_w�N�V�.�}�����]��G`�(�J�72B�T�wC.改:!�L��T��`;�c��R~�.k�5�E��s�-TK��
HU��r�r��������#0�=��MKg��59���YS�QC���'h�������|2�gAE��MQ�V*�B.%�˧���������I��wL�X�B�ۣ ��=�xѸG�s?3���-������Ɲ�����?c�K�����?��K�ï����A��S$������m�N�6#q��|��Yi�0��R�Ju,���]iL��X�]�H�c�[[֙�
����7� ���zƝf�k*��̅�Ӵ}p��W�nLKt
".N�x����v]�/�B�)%��^�T���iIFQ *U��7�9���j�`N׷���w��|@�V�����l�K	�f{��Ya"�D�[m�W���Ռ��D�P 4`�@���R�g�c�ޜ
�f�V��ܥ��x*ٻ�>���^��o�� ���⬅R�/;����,ף��������{I�o���� D-[ �.L7j����)r�D�%���#M��߹��ç��*r������;���]xS|��9���������dz�hћ�he�;J���v�&�ݡ���!6K�����@�M
�S����7t��ѭ6W�����G����؃�2V�Y�Yk1ur�m�񎊲n);!w�&�h�����V�(ѩ�%�[SG����d�ݼe\O�;7A^���G���T����P/��Ч�^��.�cOvRqZK�.j�z(�+��>�UkQ������z�����j���-m�S`��eY6��[e�8
ɔ��SR*�#�Ț�%�wA�=+��@yln��!�8c�2V��ؙ�4���o�����잳nO"О\z�]A뭈F��\���Ɩ��u�]c���kV�qe�u��{ 7�wJ����d|�K#���r��|�w��4_Z�qA��߉:�HO8������5�k6VҲС�lT��O�W{�U����^�rM��x�����P��3�W�ʤ�ڂi�W������ �";W���i�^�P�7;-;�J�vU��c�Mʔ�Q�ξ��-��k����F��`�Ԕ�.oRî����������$n�*��c��)�F�LŐ(�n��`�|���m��CW���K�y�r�NWXݤRE�}a� � ���G��n�8W2Gy&Vؘ�P������t}�'�]T���3Ag�)�R��ɏMPx�n��i��p�� �s�Y=�������a;z¼մ�T2�u�}|��xO�χ�E�f6�E|������wu%����|��J��z���!���Qv�(00�Ψ�m�_J�դ�o�w�tG�I5�MNS�Pb�f�h隌��{#Օ��mwi����
O�J�n��n�[W��8��'����H
B3��Q2^/������\A�g�[ᓂ�I�h�_�?���{��.a�%�Q%�j_S�7�D�[VE' ��3�y��\�)�w�x��fcd��	^�/l�>�³z�mD|��u���@z�*;����Лv\^j��U��D,n�"=g�;��6wR�c�~�ܪisL�y�mK����j�-%S)�䐺�6�}��1 p�0Z�:����-4�܌j�	˱G��
sm�bG���ا�*2��������f�|��rL�}�`Q��K�e��Ori�f7�br�=�9��m(� ��&zz�����!�tEu�$�w�4o�I[�i�M��$]&KY'�l5�� ��sQ'c�� '�%�_+kL}w�e�K$^��M�E�f�D����L��j���7�;�X����q��<�J4�|�jR�_9�T4O�jO����%܏1�_ja�#\]3��j�!C.���,r3��[���#�]�1�1�Q�<c�}v���t~Q��W�PJ�pj�ɭ�l%}B�]J��`�J:A����PLy/�|)C��Pzr�̻~��ڤ�e����N��O�O^_�c�xo�G)s��;#R�_�$,�@K��X:��~]�5��o��#��*=ҿ׶XH�Q���r�sz�i�c���Ɛ�e ݵ�գD5]��D�|J>�EB%5�)5I�6 ���z(��lѱ��'mGh�d Bz�C�~0LIwW-U�ׯ��l�G�PLk��' a�W�o8����7�Y�y��y�j������=���`��(� �j&/�77����q-i��G�9V�(o�%�g5���v��ʮ0��G9F?��k��B�}� m�	��rɷ���Æ��a�rT��oea�՚��%:�3l���(����<d^�/��+�T�D��ŵʫ�Y�ѰD �К�TI9S��&ń����~g�
J>1�?QV�Q�ٍ�z4�l����C�b�\C&����;X�v���*����>�p^Ns��c��w�.Fj�W�#i=6 eQ{���!���Eo4���o��{):9���G���LZ����Tv����
e��u�3x^��>��JW����=�/S;���6ho��x��ۅ'F�-Ds���_wi�}>v6���)����{	��dJ���A�Ms�D�V�;F�?����#gi�^ז����4��9<�bۧ�>�O����=�]ʞX���H_c�|5δ����x#�F�Ծe�}PR�y�o$G^�>/�R�l89��:`4�7��!`I��?M�dP{^�5�mj����[��z:����
Ǘ���~p�M��| hQËN?�÷���Jď���560�*�@y��b��F�DG�9�d�]�_m���f� o%��x#����� �nu7ާ
��YJ38?9�O��l��x�[m�[x`����,��ц�J1�9o�{�w�����B�F$O�AxSv��d�V�1n��H�C+C�%�?�Ïh�<
s��0?�D�M�Ʈ �0�� j�Po���xU��P�OX��H��,Ay��`���<�o����prbat}�^����%+᎝$bq��WX։�>�N}ܵ��X)�Qy��ol�2�]�����&�x���"X�w#��kM�����	��@���Lũ�h�~uj�_��z�##٘�K��Y��p|})��eO ��a�m&�)��˽JKG�8�A%f����_? y{G�>5ٜ��L�]���̴(�km��_<tt&���)�T�95����t��7�"2��&7�!H1kkf͂T�DE�"~���d�*5���zͼ��'��m��Z8i˓�Rycإb��N��������ꥺPL���l2��,�b5�w���e�nh��]���k�.J���S0n��/9ta���j�1eלIڛ�C��2�w�'�6�*Ee�2�DgJn5b�5�SQ���B���tW9���ʹwџ����^��{Ծ,�#3~�2��ea�/#�;��F��۸���L���Cם��:�J�R���vd�:SK����b�G�+���(��!�zEL>���x��s8s�<�b%��_����P�R�n��K�1�A�?%�IK)C�C�/�[]�x}����E���f��Ft]�:1�1�NE01�ag�њ� ��h3y"�!��ݶ�"�Չ~Ě^����qtB �0;K?��F�(���M �V���r�,��'�P[�D?uƜd%�,�b�99�R�t���h]���)*���'��k�E��Q]�t��+�F��=��}�wl+��dt�!t��%H��;���_Bδޓ����    ����l��6�1������`_+���m�����݃V�YQl�q�^4�Y�����*�>:~��y�(�t�P�[�E����+�����?eY4����!:�1L�ͺTA9�ބ�ޝ۠���|i��Bm�@�&�[ۀs���_x��vȧAJ茪=5lH�=�}�!$]�/+4�ܵ�E$��M*!}1;����s���A���d�!�*�j��Rk�F9&����쌣�;��:�9_��T�ؒ+}�'r�*0�6�z!TG�O9�� ��{��5�$�ċ��Í�ӌ��k���8��(\�"G9�D���Qfۍ�m�)�j�&r%;Rک���l���w�yW�����L�G �Y1�ǲc�.r n�>�
4����T�+*�Ǻ<Yg�0�)211��$��詢��=
\��,ae�v���'��Oރ77z���6�՗6�O�S;=ȭ�m9Qճ�_��.�&���&q� (Ϭ�e�~�����2@;�� н)�< DBN��s�+��s�*���ZR��>Q0�Q�a�$^��N(�~8N��:.�Y�ϸ�����_j��/��������+�����_��׮o�ıo|�;��� �<������WGo(c����#�z�*rl̴�ͧ���1�3!��qI������)�Hmv~�f���7��#<��_��	ͤ���e?��`�c#3=
�=&Yw��9<�$�]b>jq|?��o)*�Sv0��xRhP��
���8ϋ�-8�К�'.�u�Xɴ�4��gr�����#�oNS�*�5�����YL+��ܘA��N���~��#-���9��B>�������MthE%k�鐱��S���9bz׽q&�=�3����~Ӄ����� �r�y�f�Ho�*�Q��s钺�A�P��D��+�Վ8]%�CōA��|F���ex�Z���AF0��9�P�b�1E!y�3���A3�ȩ�C�L8�MFUJi���^R�,��d^��]�pw����gx��"�p�q'��d�P����;c��[���8 �XƢ��9�XY�g=�`�\��Nǻ�%�r�L?��c�K��by��4\)����H��u<���=���R
�Ɗ���oM�=L�sm�o�xV)k���wY	�6)��Rs��>Al���m��a����}���*.>�ʏ��-�7���p��7�nm�%(�O����,
n�_�ZosHZ�� ��'�Gu�C½��ԛ9E���A��ݫՆ��\�U)���f��T�Q����@�����)U�7��R6�tj��暃?DDq�6I_�(�S�k��wlכ8�,/��y��I80���3דBg,�?���n����k(�:{�]������Y0�29n�[��!��Qt'(7���݁� �+1�a��ߏAp���F�JQ̎hO�hc!���(��RN�Kf��/h�{dz�)��P2��<�8>lUc�_���:�`�H�e?����p<<�]�"�I!�҉�!�Cd���+�{֢wi�-چȫw�j��s��|�R^q���;T�Eu=f38�,c��4%q��B)�[G�ME.��4]����*�Cm�EӚ�,�C9
%J37$��D{~����<�� p|90���v��Xӓ�i&m&�q�ƻ���dS5Y[o]ccPJ��GЬ�XCLN1X�p�:g���]{C-�]�8��h�w��RF�����a�f��A�b�R:1��w�O:q�~NX/��8~i�9%J���L�U��E)��D�dm�E������x�NE'���M5�lt�}�"孃��Y5�8f�>�_���F�Y/���nZ��f�b(��[�Ta�o\b	.� ��wה��x�x�Xæ��ι߿h���7
���愭8ʚ��v�V�n;*b3����J�\�
TV���sT�`����6������ZW� Q>v����@T	d�38�R (�r�y�3�w�S����d�w܁�'me���V���.�X�A%��Vw���FӸ������\P/�����=�*l�yK�o�ɒ��<\�_����W��3�Q�t�)9S�S5ek�����n�O>�e�^)ʐ�ӑ���3�1�+����0��o��ebޫ;��F�wf�6��z���d�E-3Q�^�4VdܘK����2����j*ٖ�ttU4��P���tp&z՝ͽ��%�r<�Uv:�Hض��\)�����?@��F:f�)hF�X�"�q��ż:~�V��-'�%�	�2�M�y��p��vy�o67�ٮ�Л�TJ%k��t�V��"��Uwe�>Jp-�0�#���1�aR�	-=�SV�*3�B`N��Y�Ʃ�d�Y�7���ۺ��b#�.����0Y��|ЯGC��G�`���W�,��}jX(�SL����mC�t ��z-��!@�AD6�^$�n�"+^�Xdej�b�q�{o�6�+�^���fot2"his��Vk���9*U%��s�b�R������c/F��S�{mW-m�şcO�nu�kO6��lGʙS&Ϭ�)Uk�s��Z���
� z�\Y �N��H{d6�,�=�<�Z���p�;���{RPot��q��q�wGa{�2�8k��J��[��RiM�Re�g�kX�?<��=��5gs�z R�4 ^̩wp�<�����|vؚ�S|J���,�p�aF�t�-ݮ���s5�.7wQ�K.s1�
��{�>�bB��%�U���ÍJmJ��)�S01�m��2!�ꜱBi#C���C`�N��������jβ���j�ޡ.��
��1W*�yo��$�g���cXZ��|nȋ]y)2�(ZO���] �q��O��x��}�~0�*���>fd�&Î�ݫ�͉7gY��x��`�%��c����:�t�^;�;��4�a�⍋��6������v���I�E\�#�����┏�j÷�z�A�܋{�'%C� ���!��o���F�qS!%�=T���X{_�puO�s�RQ[���V~��i�
ۢ!/_'��h^��H�Z(2p1�G�<�}|4��{`��NXR�r�_v�F�Q*hXN^'��!��k�l�x�>�J�ݖ7\��RjknV���aU�Iۛu�d�yc�����w��	���O��Si�+�,����'|8Fag��E�����^gܛ�[�1P�E	I3����tR�4�̉�
_AC��J��մ�0���~�#�^<�.�5��# %��⥹,��U�o�%mj����."bjhU?U4
tH��)0F[2��H69�>��-,���cFw:���k�\K�r���m�|�8C.��B��d*z�&0����������c���XQ˹2��]ҭ����f
&
�ckv=ԉќ�Q��I{J�h�P�X3���5�Y�NƄ���֥���D���EK)�S*~����Y���cgcf��2�ѕy��EM����I	�]&���~Fv���bs���=sڐ_f~)���@�M�h�eCrLݎ�3�;���W�����	�U�J�jk4�+q�]O��*�&qS���Ǫ�!�ke� �`�$]ͮ�<�zT9��!�dc>��
0=��{��.�1�.7�b� ����ls��u���w:���`��g5h���w}��aFZ���k���N���ϼ��&����w���W��[�=�n���ܰ�~w8��7�z��@`�dK�S49`h�,!�2��*����UR���L�P��c8�u�h�BI[�J�g��S���/ȃi���J
 �Ԋ���ӂ����W�dx�S)����l�b_)q�dT������ZM��V;":�X��c��&aj�jz��7Cr�&P%Ĩ2]jJ�"�lT).�[O���Vq��\O\/�7w�{<���f�u��������;�]cL��d�S��e*PZ�?gk8q�IF��i�i�
��/B� �w�_�����o?���_��_�㯿���~�������n_�r
�)V�
!E t���{@�Swt)j9 �o(=w�rB�
�qo̽��/�J��-$��gZ� �n����(�[�u�����-~����j�-`�'X����W6    ;5�pg�@�=�<۶1K�rmG��po��bR����,~�L^�Q�^9J��P���7�R�뮥��54J�$4�s�
0��r�'f�������2���5����"�Tkk�:�ӉR��0���-�����@�w1�n�>auIWnZaF
p`7Ő��yr
�����}��3.
Dg��� ��b�C���.��a�3)���������l���C�S輞y���eSq�B,&SB�Z�1zJ�*�L�K���B5%��s�������ۂ$\�ϰ����=��ҢS#\!/�ٟ���ߔ�טmսŀ��7н��D�i��������h�-��Mo����0�߉V=FM_�G����W+yu�� P�,���JK-�TX
*�-z��}G�y ���٫�zȳ@��b\+�*�kb 1��O�}e�*��u�e�eOT,>v�����
���m�yY����b�Y{��0JK�
O4�ۧ'8���T�����TG��g�������>X�VOY?}��a��!�qG��6�f-��t^[�����/�|��w������?��Y���}��K�5H����%��}�����W���������s���ۿ�����������sۭ��7�T�������&R�8`Y\��]��������S��4���e|��Gi�O/����q��u{�7�ݍ�����T��\s�(��_X��"�Ҩ�Q�o�v/� 3؍a!�q1�����a��Ǖۇ�m3�Ru�wb��~�= �(��{`��� �ݍE�<��]�y���k@��`�`��4����e���4@
�v��י�����>6��%�"��������c�P�sq� �K�0J9�9��b�9ay�����</޸�|�����D���I&�]��r���W�83�y��%�?#�� �1�R�`�_	�O�4� ��������M���fi_s*��"�`P���r���q[ڻ�A�Y�� �|b��J�����_'k�&Q��4�o ^<�t��b�q����2:b�G<�qП��ˁ�	���C�a�g�(��<6gE�В���Iˈ
,�S�����	��cɩ8A��G86�gw�����DIߩ�O
nA=��mP>F2��(��:º��ֱ���
���}@��v�[�NA>OS���ȍ��O�q嶬�2�X�>�C�Sʊ�ɟ��f�#n,bw:Pw���d'�M|zʚ)̹�ϸ�}|���/Ot�s��&�k/+p���5.[\�8��;���\�,$���e15�0��d����<w���+�a�X�6���OM>ȭ��	[�"P�?���d�b�1�ɩ���)B�4ΓL�&1����>|d��Q��s��-�^��Ϯk�Gs��۠�s�"�����\쭒�]����}�GX��ӈ�T�|�R�R���3�I8�O9=�9�������7G	g�=��xḾN���`B�N�op�B�3���y�=N^=�	�u�o�c(�4�5h�[u?2V����_`S/F�S޶ׅ�κ�![����K{i�A�z߿�FrHϡ�����zY�������y/�/-+A7���#I������y=S�_�� Q��Pˊ�ώK��l�m 55� oX����;�ۧ���\
�!/�E4�����惼�����[�>�DVPfl�޻j5~9��\+.��P�4����Å�F��vꭩvR�㴝�|���+� ~�σ�M�2$b1O�d+ �牺|�F����Pau�%s��0ቁ���[PȽ�1/ D���s��r���d�~C��'<�L&�c�xhV�e���a}�T��M+Q����(��8s����;��3`�dZ��Y�<���|���;�k��I�R�������:��<q���)Q5�i`PD�n�ob�az���h%<�t�㤅7��)U��)~��h>_��gK�'�S��B˄id�V�&n�q�iL�r�a����,��/F'�����H7NM�c[��q�|�deH/D�.&EƇ��	f����$˒p;�o||B~�e�N���fw�j��1(3'�d��\���g����JԺDtǩ�����W�{0�N{�x?5� ����������y5�
�k�R�C�����5���ޗ[*!�J2�b�W���o)��	�~�n�d��F��t������)~K&��y���=`/��|Q�^{,���D6�'�{a��w�B�9I���/��ֹ�x��=tX��ժ�P��ơ$���r������a�Q��Ƣ k�T��-"DM6��>��Pj����cWB�,�����A��tt{���&����B��F	�z�xx)��}��"j�,ɒ5j�w|z��y�Q�Ψj�y2#��RZ=t��ӕ��E�r!L<l�:p-:�c�m����YL�Ny�K�?�[����].Lwӯ�����|�D�W��������X�`w��u)y����Q��U<hF��]&֝���~%O-D\l�SG~�T(���ܛ��<:�)��K+���@��Ԑ���0t��q.������h��3!p��Z���}�oj���԰Sg�5���e]`Ny1� ��[ Q$�@�p�L:���)q<N�����`��V����[t�\�u����f�Û ��)K�M�LCc��j��4� �x�nU��������d$���3�����b�򸣭~��Ұ3�+�b�G���f���e�B���n{Sf|A�2f�9&P�R�Խ��81����$e��b�c��,;�Z* �M�L��§���T���Ʒ~��S��\ML>�S~�n2"�`�Lr��j*������	�o�6�	�Ņ��P�^����?�|�Ĵ[l$ZnڞcX���D�w:���,������X欢�ky��O}y?fr"0�'��m!m9F�����0Ao ���.��[K��8�x#sU��=�fw���(��t?��w{����ocb���7Z��;A-?�ĉ.�r��D~9r�p�M�n�O ���b\,�g��ej���԰�.Z���3j\otN�)�lq��_)��9d���%o�pyks�4;u^����6G}R����WZ���1�8�琕j-#���$�&8�F�=�_�uSO,>�+�XJ&N�pW��1E����]e�e�^�m���0"\WmGZn��n%y��\A2��}�z��_�!`,�2mΝ�Z�Rj����H�|s�F�,�s��:K�;>���ǉLA(�(��{�}cV K�9=��"ytKȞo�a��Fp2�}CCy����N�_ʸ��^���H��^%\���Y�ڈ������EK����� �NBֻ�b�Wy�Wܡ���$C����y��t{@��̂9(�AĢ_�jw�w6.S�ܱ3�;�?)lqL��l�x�"5�z��( �wN�	���z׵�'\�+C��d���#�Rыp��#߻j����\=b�y�Ҧ4B]2�%^��f�n?���a�0c��$�����˞�s�K�<6�縤�����4���������LSs���a�S,;���z�p����j�M;��OU��p�Ll��8�c�Vo�C�kO{�	%vS~"I� �q�J�%�ڑv#|d�gz"%d�|�v�k2V1	����_P=�O��XO����M�����*�����C��X��%ᖐ&},����n#�y�T�m�V�Z]N��-sq#���n��q�x��P8���_0� c�`��Bn��0��2\�zzvI ��<��l������\h��E�����l�'\(��sէ2k	2�����m���aJ�d8�[���I.G� �4c�e�8�0��9<�Z�1��u�y �uwQ�Ң}�v0��{�P=�}��]�wj�q���u�����ݗ����;�����<di�� E���]�׸M�v9�ӧƜ�>"��m�H��K���@�g��p}%6jJ�e��G��]N���&��Rwey��F5�4'��W�1+�1n	8W�[�8
*M�q*��a`�f����{����cX�n�\��ڗ�")v���R��3Nh�ƚx�2~��$�V    ���DS�.[�~{��Զ1�L�Z��Mm�2�6@k��ݖ CԊAO��u��$���0�2��������S�����]/%0�����E�5/�4��q,�!�,�˶D�:J''#����wgl7 %�j���0��tr�G*���%��n��P+2��}�p�G���d�Z�$ꢭȲ�S)c�_�ݦ%��>��[e��J�k����)��_����0�%�n3���`�B:�+�j��:F�k:�Wi�ׁb�g���B��wZo�>hv<�S�r�Ǡ4
똈_*�ls�踭�In�ϛPT^�_�8p̫��4nXf}#w��ϟ��MӠy��t,�ﷹ�gm�n�S�(�7n�D�;��Ƚe��z9 '����Q����;Nn���o{O,�c/W��g\v@ܡ�j�����x�2�|e�5�Y��ɼ�A��4� ����pq����}Ѝ2M݋�<���d\G(w��zP!���w&
UR0��/&WW�κ�Ce��G�
�;�K�����/�+�Ճ��1@n�+U3c׃6w�Hw�S�v�#���x�0���m#٨�F��b\���x�$�uv��i���{]Tm*�C�MM>�3����g&�ǉ����:Ôu���g�u'�+�A�*�T�mőh��5�;�u ���ɴ���Iso ��ąҮ�0���U��@��c�e�l�ҽ�*=��������C�V��"_��<�{�l�IOS�8FI�'�2/�;�+�u�t�Z(oQ4+�QFk^p?���M^�"�>%�\�!ȵ�w5�،��,N�9���k~Zr�i�S�g��P6�/��ђ��q�+���u��5�����7�L���z����{ų�e�ޚ0G�m&c�Fa��>�|8��tr�`>E�Q���4�Fz5`S�����Uh>P7���XizLGo^�~��'����� �uk�+q��_�Sew��SV�Ur�t������ź̭+v���(��5d�+���cf��t�'
a2����)�v���+����oR����sks|�3��~j����z�D?�s̂np ���k\jʍ�>�:A�C�v���#!�>��&ޖ[v� mÝnC��p��ܫ�t�b��wZz�J@��mM��M���Y���$�VO��d{`[n����:�
�*%�l�3&G��`���_)�0O�Y�ǌ���pRI W�:�.�)��F�h���>孾*�b�q�r����&�d��n�z��œ9sv� u ɐ�޲��k]p���y=e<���ۚ�c����.cF-�����x7������V�G�!mg3?c&�;��]G�@�o�c��W(\%�\���+��F�H%8e���W՘|�h��d��JoVOZ>Jen&��=�*�6���S���rL󃒙��U�I��odkq�� >k�R����:K?"�d�lk�S9z�nC�B}Ol�*�k�b�c.jEcxcf=���iV�<�N�#2ka���ݗ��N��9�Vg�D�2~6|�����\g+�v����Wqkk���6�f�L�$��9P1�5,�q��vٲ��ʍ�\Kq3���r+�@������	����q�o1�.�R�]���ts&�^���X�8{�Al2ǲr�/N��a*%�&*�E���k�������C6�Fv�@.2�0��DPmR����2��K\��Q�AO¬$�����@��Մ��T��e�B�礂Β���<��Ɔ� �����d�W�V
Vp ��7��ܯ�K͌!贂A������0���2�t�b����𥴍�X��.ZZ�ͧۼ��Á���*���'��D̃�D?�1"|)]#�-���Q��_w�E��/>L���X	Xg!�Œv��M��v��T~�rYAP5�C�f7�_���ߺ�z#U���T��4��� �b�A� �wY�z���G=�_��(	w� �UzY�2u��~�4�+��𕴌6zS�������/&k��a�RjFak�Y�۸|ȟ���* I����Y{�\�l�۸SHy��G���7_e��(
(�7.���N��%,����e�wʩ�3R����Q�g���`�P�pMC�8,G�p ��1Vm���x�y��p ��<�0uy��Q���Y�$�Ϭ��;����u!�uDn�EL�� j��V�M��C���r3t@zw~�r#�쨗S42��	Ϙ��
�^�cA1e[~ZӴ-!�b�q���@5�e�������Ԝq@df�x�<��p�S����v�-�| ���R77�u�����9��.
[�D}�oX��z�X{G=�"��A��혻h�����������b�q|� v�^��r�kC��*��:�/��p���u����Vc�D��%���x�M|�C��eB�I��D:�(�SI8��b�%[��fi�S#y�Ń'� -���q��h��/.L(�**H���3��<�3���4(�cvU%)Ѫ�t�l>�(���fa�W|�[=8}�&/��1mr���,���@�&��mHL�>� B�k�Z>����m�~0��S�	��+��ƽ�B�n�.��������rI��o�S�*�n�}��{��f�{U�0@!of^~H�I�"���f�������4L(��uҿ�옝#g�X%�x����
���M/T�N�Z�e�Z�V���X���y3��.�c��lS�����Bfr�wK�srt����$�	�b{m�mKtI�F��k+]��A�+!Х8�P����5��ݕ3L�=�Pg�"�'���!/(�ES�E���u��B�a#�#$�d�TU�^�)�1<��"#�0��_Ý�o�x�qg	��(����������/��$q(��K�{�p.��yI��b�4>��)W2�@��1�H�7ÎM��Q��G��O�F��sn?;���,K�t"�8�� "td�v/U��nS�)ƶ-�ȁ-):�BVFFV/,OK������jG�d�{�R͋|*���{�-�[�ˏ�z��H1/�܉X���<�.��*޾�,�!Y�)�jI�ѕ��S�-���[��d6�C;���Q����0i�����~Ny����0�I�*`ı�ߑ���k�
͖�B���,�)�Tڡ��(�ކ&�ԗ���K�����zʀZ�n�@܎Y"M�fU*���!g�*� �S�qY����`]���r� Rt\|�0���P��(SOq�@�� .�̄�r]K�b�wl�h�$`�o�i:똴ݚ`S�\�-o��Ś/�%�����w�:�e��H����I���ci|ѷs��5��'n����%��J��:��HqZb�,xt�?vrx�Q�A׼}1�
�@E���'�b61�Z��ɶ��:�vRA��UvO�r����Ʀ�l�!�<��<��Kė�=`�����������+LEiӮ�u�vk߸������/͹�Y���N=�Ԇhh
���Q��Ra�<�Ւڑ��{�)�5iҒ2��3N���6�Ď���%;#���/F�/)l�W5ױ�lۃ�]Y�a	+�;$��ybv2!{2=�$�j���8fG�k7%�d�����& )$�-�n��n-ds�4nWk��6��JQ�P߸<w�Wwr�o\�t��1��C`_���S�����Ɓ�S2 |e�U�S~�� �Z `j�2ɜ\?ʵ�a�\<�GUĘE:�4���/T���n��"k��"u���X�� �qsm�Ȩ�'��l�����'򱥆����A�6�֑�\���9ZT�lU2rl�l��5�f5T���a�6ɶ%w99�g➞��Q��#�,E�V�s�������lI,���`f7v�Ȍ��&oʄ���yX)���g6ϒ/U���jG�Z
{V�;���9�V�X3��^(�e�ݤ׿W��28�y^��W��jZ`�m�����64t	�Q�@�M�ˎ|�n<�@RS������k�e��r�4?��%z�����b�"� S��M=��P?�B��[ژ�+b!*�.2�#�U�&�M/�Ф��2�J��T�1�Z�Ir6ɻ��rF�iHc����b�dg��j�h61����D_5�^��L�n)Zc    {q\CW����o��mǝId�*M\�/��l��0�ї?��E�j�5S'�S��[J���*��-ժ�4�����ѡ'f	�b@��C��tO7���y�?�ym�2A�,С�=Qݑ�K��UˎQ��&¼�'���k�:2�H��!;�)Sу7v�Km
���Vb�+Su�q}c�(ZHƠj�:��5���fs�$�<y�:��|T/�%T-B�̺T ځ���*�#G7z�`z|Xu�Nc
��D�B=YU+W�������� V�`��wYd���{-��d[�ĚR3�E\5��JZ���#TG 7�H��j2�4)�x�lv��N�3]\Bg�
�>/.�m��̀�u���\}eF����3Ǜ�>�d����n���tL��!̇�(�@�y�U��l�[�P)��X�M3EI=��TF(�����L�ܸH�f�uܲ�b�Ez%�1)�l�C�~��nܘ4*�b�-^Ʀ�8�L�t��+b�F�<��0`cb?��O�NتV1ӴM���Bw#F~��H�r�C=���:5���Z�c���a��W�f��L�w���uѵY��I����H��V��N�qL���k/��S�o>���?��Vo(�Pe^�O6kR����5����[�H���ħ�	J:�^��n���� H��r�����D�Z&�� �6���p��)�$�)0��ƖV�?� 1oeK��\c�PQu�%�QW}OG�[��2�NM��4֗J ;z;�,r�,���$E4���y	�f6�B�P��Fc9�y6R��<�n�@0��hϐ�X��Y��vZ���_�{�	W�C�s�K,3`�E�b�?1ś�<��8^o9��}a���]*����S.0dWk�!t����v���`�PW�d���0�6Q���Պ!Ŵ�!��h_H�UAw��O9�M�BO,cdBm�12�b_'��T��#�N���2I6(vQYf�e���rk'ZMSKH�:M�{�C>ɤ��\J��W�p������ެ6�gW'�;qbjP5F��q��@&���;��sK��V��I�CgŚr""�]��4���z���5�q�j��Tp�A�{j~�����ѕ����\\qגe�@T�u��+�d
˫�{�r�r� PU~�]���^�E�})�[r51� �>��K#Y8���'}Dޥ�+?B��)!�>B�Gw��G�U�AM�Ȓkd���	�׹�}�s2�jQϪ��jW�|1�D��n"웚��pN�A�:^���ig��o�R�@!���U�g�2s-�}=uM�r'������﮶���{�k�}T?��I)!���ܾi��+f�Y����F�#�~�� ЂR(7��[*Wj#�sL�5f�(��N�U?)ŕ	6�)C2sQ�~��|M�	!1)V��	}�6?ժ�3��X7`G�����I�J�u�ҟ�Y��g���n�t�`E �\�k�!�:��h5��2!���s���\YI-q�iE@$���&e�!ߒi��c1�*�"�_\0n[�rMP��`%k�B ��
�:Uw��A��J��fU�U��N1�i
��1�vĖ�"�wQ��-�-�kH�^Pu���U���"<uvǭYQvQwn��,g�2aQ�!{��g���9>u��r�<1fuH��W��#W�MD��c��)�,�.
C�Y/��,R�����i썐���Ȗ�,Eb�^[��N��Щ%d4���S���+��ȢE��Ae�����:���Yu���xAg7S���V�m�l�} ���)�
؁9�|�He�wFn	l'w��� a�}׶�m��� [�[��{94$�*�i��T�]:���ק�(&$��+��t�IGq�v�&踱[M�U���������M�����79���GGv\M�icf/I{|"W;��P�
w�*�N�s�q��R[l��5�b�]�@Ǵ����O"��RG�/)�9jԢ�wj�F�A���5��w[�܉�o�-�W�4T��4��ȃ|��Hs�H��B�z��/��B���J)/M  _�'��4S��h'L���8{�kjb&���/HB�Kp;���ؖ�E�%ep�7���֯�O�
��gڠ�*1�v��z;b�ۜ��e�k�f'�]U�}���[�0#�;VW���P��_k}<��� 4Qp��k�������E�w =��ѧ���q�v-�m(EL���(� ��HI�������;:�s~1L+ /	��YB����q/rc<��D�T��8OLE �����	k�����JV�%�è�j���|
�o���&C�y�t���k|ѺV����6 Y�ćV^V�3�0I��x�9ā����P��S�ǨY$=B�$��K����㍆�Y��\�i@*LK�����R�0�=���K�.�D�O��'�%׀��$g\E �\�nW=���M=*�DD��R뫥����]ݿc���&;.\��#�dѮ^nI�ZP�ݠ�ˠg�xrk#Y��ɺ
��-�Σ.��Eyb�%Ő�^>N3>n�j�ⰥMm��Ĉ��� �Ȉ$��Z��#�N⏛�<�B�o���k%�����
�n� �MKZJ��]��\P'���AƠ�%R<��� � ��%j$D�O�U%�4�L���,؆~�E̅�n��/%g����b��<�t�;*`>��|J����t_�]��*�÷mF��#�q��@���z�u$4�����X"��:�܌�x�s�(���VAml�5�%;R���� %Q�a6qK*�M`yo�uĉy�[���;9�7&O���uULN���fM@v��~`R�41]h�3)��Kg����N���6ڙp&.���=����:�4��C%=��T�eǅ��׿��M䚨䬬w���vz,;�K� �
��C�\ڋ���u��Bo뚮#f���l(}�������Q���5���czl���l� n";R�L�%�ub��ܶR�><�'��Ʉ����,��M�`�qbM�Mx%)D,��nҦ�01O���ja^�t���D��ܽ�U;t�\�I� o�!T�Փ�QIe\Ȏ�?�(�k)�g��L�lbhb��%{)e�%�Gĝ��I�t5E8�ez
�BE��&�v�����H"��mST��ވ�Dȶ5�I곩ک)1]�T�m
3�N���Ӷ�Y=Q�+~
EEN�<%��l�[��dg#Z>�[h(V��4y�%�"l�
5#O*�(�ٚ��Bf��&q7z�uR㰎�a��{]$� �tU�ї@�"�銥$��dR�=ѡ!T(	��1��-����L��譜�d(��[������9ȷ�{P% 
ᓲ�EP��R�ml;:�>����ʞƿ�'�����Ǵ�,d�K��-���4*?�4�hb&��mE��C,�O���ڙ�Z���*Cs(��n��c*d�[Ʉ@k`^BW5LO����1F���T���}8)��Ƴ��K�+��O���<���Gm�EL�+����QĔ$%Gr�ogoq���&c�p�fص$n��L�-_�ZS����c�G�"���,B�v��b@*��A����pa]��w����R#A6s�"�0�\��BeE��c�O7k�Џ���{��/k®��K��J���-.p�z?{��X ��I6�/���ч�#���P}z��X}���
fG��7�5Y�t%�H�E m�<��w,����1�]���C)?U�%	�������J	.ŕ��R� L��m[ٗ�6�[�HD.��)���&}=�`�Lq�Ja	r�{�2�����[k8�'�C����2�D��lMT����C���j`�V�6�ԑ���'4ȃ�g)yy[��u�$�[�cVA�s���y�^��4��K��l�ZS�ЉcY�����+e�����@G]������#E�4?$�B.H�i�5�����%I�+�b�Pk�F���0ڷuGb�.��d�mU�u������I�b�o����"jI��i��b�+Z�����n�������u�	�/�bK嫀}vqϚS�C��"Wt�q�i�����1۽�I�qL�g����<5�~&�mm$M�7�񦴊!w3��z-$[�6ڞ�i���0�	Tf��`&���ِ��,��A��¦��S�ܭbD��R    �K͌!L��yDG�)u�4Am)f�Eֹ"f�������(�U0��;]9����x�5L���0�Փ6��T^Eͩ��ݣ�J�MXHz�5���4q�i�E�)u��Eh$d�][&��Q��I��	ӎ���3T[�u̓�l�����ޘ��7ǫ	s?���H��4�0F,CnA�E�PJ��dO'�4��y_
=���vgǭ�����Ѷ����V̓�/�e���f�A��av�CV���3ݗ���2j�%���{�Ɛi� ��v��#ߴȊ6.Q���2�d`����c��ss7��M,��S�W�r���g�].���,K���ְD;�Ն�#Y	,.p'���E֤�m����L��W�Q�%�5�Od�t�ON�u�+U!�#���rm��?TVi�P����z�)I�e]ϐ5��-���Z^BTh��
-R��o'���Dn��9	8�iA��i���L���$ ;�]��5%sBH��ի�
��}�SS�Qxc3�v�ؑ���0x6P�nMS�U���E����|��UnI}]$TR��P����ڍ�����LM�.RخT�R_5��ĸ�ms�^����͙w���ض@2����.
l�0��;ڸ7+�I�'e;*]�y�)�]S���t��"���H���*��%�B�f�U`�1u�L=Q�L�����)�t�A��IMG��T��י�	�|ۑ&/<����F��-6}o?��e*��06W6� ��Ü,��K{`\?��SC5���VE9RfRFۡ᥽(���)�b\F��T���f�F� I��NϤ#�QV��&X]O��S8���Q��� U�����g
'��B����ɓ�o��B%��B%f�b��lp]��ɾ���k�_-�f�^��$�kH~Q�I�-ɓ��bT뎐|}y�CxF�Uxm�;E�5*�W�p��5��������][���[�D��h����%1��褝@�~���c�!�.q/��T�%�����,9q��.�y�H�d�i;��bl�W��Mq}SS�^P�U�'8��U���{p����4�Bݲ���A���4Gb|WY���z��$/�:�,�����YK�~�K�y_����5{�|m��䞑ZV���Ȅ_h�F��L��|w�"k���1G6�p�����1���*�-eR�Sz��p%1�M�w�$as=3�����)���<d�4K[H�^�V�$Ѯ��'�D�h��;���Yߎ�:rθ)��ؼ�e�~�LMU�nf��t�!����\�4K�&�:�	��o�~�.++���
��D�O���,Ҍ
���A���A��ENm�'ܵK=���K+�������k-���ID5�=!{��ж�#�[���c�Jݦ�4��Q���G�P}�AN�z�Lʗ�P���*,t��-�h�,*� .U�c��Գ��_^��lgM|�N]���+PH ��D���(��(�D\y�K���Ӹ��c�n���3�y+;C-��(�0K�RaD&��&��8�E�vb$�à���� .=�Ϙ=����l���0�lR�x1V[��Ɇ&&Q����3;��H�-C���2���zE#�;���Ȳ��5�Jy+��#��Cm1ÈB�oS9�F��Y�԰�iR
f_��r�B��b�V�[�6J^I�.d�m��0�lO�BG�F7��X��Ì�K��@Jb;S�X��v�߀�X�uJ�J�]�U�	P�^% Qy1�j��q�q�j~3!��l�nJ�)'���t~��#W����Q���������O���02� ���N�uMO�b��x���J�̪x����"b�?5�n�L����\���}n��� *W�Y�$`4]�Z)K��ٗ+���%�'���<����fu:�2�~~E�35��g�H�Ɛ�3,]X��Po�,�0H1�0�f���eTH&&y���E;^�ј��V_�>��z�.hC�?A6$CJ�Kp;�Ω���+��]��O4��p�O��.(�@��Ms���g��L��*��Zx�U�D��&�V�u�k�PݮYvds���dG��8易�I}�ryl�6$qk�%���־5�"���3`=ǭ��d[�X���j����'Z,��;�����I45ū�-�P�#0�k�F����zxSFĲ�����i��� �m���l�V*X�Gzpܖ�/�,dց�{P^�ٶ!���y��o9�u������Lcj	J�[�-�A��ε�����l��	$��I�ftk ��vrS�Ĉ�{Q��D.�q&����<�y��u��Ʈ�*�~n��BF'�m8՟��$�_������:�U�E�~؇�|1����9y�+=\�k�h" ����+n�&!A�I����$Ee,;9';M�[�Y₞$O�ɓ2��$�k�u��d��U��RY���B�Бi��4���s�0&m��tLƱ����f��)Ӫ�{i�,�P�K#M�:&�C���ȇ���i�����) *5�8P%[s��I��y5TZY!�M�@a�jcq���k�"�OS��n�34!�%^����I�v�	]10!kt�*|M���8I�U[c�UD�v�1o�[��cl��mj4��H�  ��E\M"�>�\ӒfN$�v��lg�[�K*7�_�X'�i	oO�H6>�����`�@�|w�E�#Ӽ�U?B\7�ɓj��VآNH{�� ��S�x2A�hw1�r�Ә^oO!�ub�f}-��l�f3���ep	� V���I@d��nJDB_��\����Z����[8���9���iS�d4M�-K�(�^ N���rS�d0�)��T�a	����
����iy]�&r����G�>��]�+)������Г J���tG.lI-��24:�J��%.�-L������,aBG�d�[�R݋�.�!N�q��, [���ߔ;8����ْ� zE4뵛r$�9�~�J���
��
ܭ�X�H�����qcI-O/��BG~d7�A����������DS�:�#���b���7R��r���	o�6��)32
�Ш�d�7T[�g��*ޭ��ҤT����Q��v�v��H:�h�Tu�"�O� Y:�=5��\�P4"k]���|}�/p���"�zC����ʲ��7N���^_�kI[L�sꆊ�x~��,����z&�]�۔-d��*o���1�P������r$n�?^5eS�_�� �� Q�7+ v3���XPF\I�ْ����Q�'^:�!;9�P��J��b_+�E�K~������Ȍ�	5A��.'�Ǌ�j�З��,1��ښȸ�N����G���ʖ��	?��HL�ź)��ݘE���O�m}��^�evd�����?��8|MA.[aH����%��Aq���{m��#O()[�$�t���x��9�o��T<��U27��5��zM6@S�]��a!+)=$Ǔy?b�ْ����p�I���`�)	ٯݔ%�-J����AE6�u�����g���a�W��޼;<����o�y{�	�9�!;�'�x]!Ta$Tq@�R�3x��W�I�#;��f�,��x�g�k��|�0*]���Ȋm���3E�g���*ے)?̼�OU�^�-�&׏�mr��<5��Rz�_j�!���8qZ�Si<�ͫv��f3h���|p3W�$B*��.4
��I�l����R*d�,���K���#�q��)�N6܊���Yhzz�QE�l�lkIf�j�|ے��5qK�*�8��h���M���9��}M7��k��$%�h4��.�����ig��2�"���)����z��!їC�#�-�'������6
�����U�Ɂ�]�ےMWҩ�A�P�H�Yן��x�j��G��Q��_ ���8fG�d7b Ϫ��y{Krvn?M��� HV�T��/+��c�����;��\Y��V!�s�-mL�1�����9�H/��U���/����K��ՠ�u��u�U:�H�ٵ�7e�E�8�O��e���j/S_�UK�N2}1��Y��؃*��ElǻId++M���V�Rs��3��ܗwS��@dP�k��Ta�όc�r    ����1�J.d������3ɜ��N8h,,�v���bsO���&z
=��m�,�����4jG�/�-��4@L��MOm"��p2/o�ê#��4tL�fM�s��1'H�B6�j�iΆ��\#�ok�EsI_t\����7��p'������(晦i�](:W-��mۺ�TG6n��T�v���19"��C�j���0����葺���� \0Hy�Wd큘t-��d[�D�R8҅���j+i��1���䦁2��B��2��ɑ�7k����2!�\�#)�����N<��!&���VO��+CT�H�9�,�?�^���n��1�2?������1�9�x;�ҷ�����ҳ��Iv��!�1uY�$��lg2��f��o�W�4!{�j]6f��Ƒ����7&	��ZS�c�s��t[�J20M?���u�b�Xy������'t䫅�i����`i���(��D$���H�dq�SGL|�U�2��1���0_ǫ��Am�Ј?4j�.�V�~N���&�)��y�|��T���~�����:��+��/?������}�X��L [���K���W�C��������?\�?������}��4Y؏o�)�w����%	-Ig�(��l�#�8V̓Ox�q1�͗�{C5�N�]z���n�b�{
$u�عRB�����3d��v�9I�p��**I�T`P	�Tj���Q�"�1VU^O����S�g��B_}�p�L4Ǵ�w+Z��z����{ZRc.�_��̀�S�ɒ���5 ��������Op7����؄R|�\��8+��^�!����{R�0$�m��d����1v�����j��	�f���0ɝ�������L�+���XE촌1�|CK��")~�|�:�Y/0�Z�*���)]F̪6 (7���ds'[�t����<��M!�m ��sb%���Yt��ɾ>U�1R&U��О#�j��'Ƀ�N>C*NjժNƼ��8a�,��;��k|a[w����s�Q
��	��+�Sg�:KeT�N9Ȝ�l��I�N;��$b��[�}8��d����O%��@�?�Y'�E]�i�x��Eg�^2y�Z��`��8�F��:�v¼�5���Q0G�h,q���0���o���&aZזFxP�v��#ri͠K�ca��b� �P�0ۢ�N߹�:>�1$3�׸#�- �Q���N-h�� ���hZ�Y�ښ�zB(Z�a7�N�.#x��;>e|�NrP�rމF%A�-8���Qyr'���N�p��o��vt���	�x���ݨ�A<�*��0��F׊>�{))U��~�� Ђ�'W�L����-�Yy�\g�U��m3��l�X3	6�^㋺��	ܒ���}T���e:It�6��_�uZVGzN�;��7�؎�mċ#�1 ��k��ne?gwj�s|M��5�V�=�#_���}��ti�� �*���9GT��c�F��y�4�-Yc��0�}��+MM�����N�z������C]�!N�z�n}r��I��JfY<l>j���R_(!X�fPŇ��%��I!R�=�6W�-��QuLdTp'���-A&E��gN5���'G\R|����5�I*��Qˋ��A>����<�1�!���pj��]�U��G�Xݑ
i܂�*�����uB�$u�'Cc�$�n�*�n66���Qt+�6�(=4�T��[���ؔ��RM�hϺ�/,�W�LOx�y���a��ɓ�V��"�*aV�&TI��m&"����W8�Jl������������K��ȇ��B���h"N)q����#W5$V�m�L��<1[<�݄ק.),��4F��K�߸�M��w+t�Ud�l�5�@e@SLeV wS��eG㠩�*E<��0\qM�ic�>Ib>Ɣf�=��x$~U털#_�4�DQ�&�Ğ���9y8	��YC���\d�I�$�m(!��1b�
���ځ� ��=���e!wbܜRDK��(8@%�J�E~�s�K�@bF6|A	s_F7�N��*��\K:[s�����p�yqGn77�� ��XFLH��T�N�;��n�`j"�U� O=z��լ�w$����T���ٺEI�+1誸�	�Vv��YP�s~o�\1����})�_�1�Y�����5������9�u�]rì�EWI&k�X}��_W��݌i�\�:��ZT�^N�7s�U�]C8��.� 
Y�}8����M*�$rP3
H1j�/5`�~l��Þ�N3�,�T+�8*Nu Q��J_�R�Z_��W�jd~g�l���IuC�ߎh~jp2$�(�mh�#�5Y��G�����#��1���=�@�<�~-�UJ��IYm��1Uz�Ԇ���X@��v?��P?�$�-l�E��ŝ$�զ�p?.64���Qĳ]��$E3�٩D�ORW$"�[2���2ͭ�FǇ���|������<�HY���a��1�%�[���qJ㼋��%�>��O�t�AO�"�;q�L��	2���#mqwvI����<�3�Z�{Z�ȼ*K|Ư��&+��?��/jxhu;4���R��w�%�GjM��#��ӎ���֧z�Q'	�F�1L;�HB�Z�<�W�2u�0����X�|o������C\D[H���52�]"4m$��ݷ$S��� ��ǽ���2��T�SPy�ic��\k�eG"�D����р;Q_f2�AU+�x�A�\�N�;9���u��9Lt���@$i�7�f�Rv�!o	�;�4Z8���d�"D��5�-J$Y5���Q�1kR��֫*rY5᧢[���y�oL���T�/R��F�B+��[R����T����D4p6S̋;������%������"+�i��~�_�rJ2��e��ռLBЋ�$eG��cd P5��z��RE{|���<���A�}@��Ć�&*}����R��k�f����\I��6hD�a�ZX�n��YR5'Kh�n+�4=�8S-Q�vSH����V��D�Y5Y�5�5y�r�� ���h�Ҝ���j9>nj���p{�M��'ii��_�*��H��1T��QI`\�	~&WW������L�jbhX��*N� ��k3�M�ѥ�>=y�$o^Sd�����+TT]i՗j�s7r�I�T��zZ�To�
b�5�����m}�-6�Յ���A�3bُ�˭Uj2'-W��-�0���y�<SR��;Ӫ,5+��t�^]�uC�$"�F</=Nj7D+�q��)M���[B��*����Xx���KQ��:�·
A�=F`ϸ���?� $4��	�V���P�fu[�1�Ҷv���A=k�BJ���9���k$��ў&������ɶN1F�9ըh/� �'TX��mGg��J�u������/I..���z���i|��R�d{i��IF�%T�.�ve�E�2�>�YWo� ���j�a�k��$�ailo?o�-���K*}�c#�
�tU��4D�0�AuE�4��$�Uղ8o��ԑ#Н<�Wg���>bp�v��9 ����l��)�f�,q����4���*Ѿ�a3�Z�A�x��5�<���X�1�~D/�XHb��?7�o�5�I�S B��ef�i��&"F]��ѹf.P$���"}�F�B���	r��9w�l2�	2i9�I�t�H�z����h+\���C��FڰK¶��;^���Q���@�i?}b`d���SG�7�[&+�B1���?�(!�FNKp���]�{$�@]>KhAE��j�
��
�n��+�L��b�,�K�fj=Y���ԉ�����_�-�H(�� �B����N���c���M}��BS��J2)|�5�_�*�Nb˔!�bT�i�][������~k�D�[|�6ߑĺo[tT�補�k�}�b:�zOQ�����0_�*�TĴg]�i��N	�aH}���A��<r(䷥O�O/J�'^I�b�9��G-Oص��6׸#E��>$ζ4�� ��Ȣ�\[ң�15Q�(	�^j��������,,ʒ���k�Ry#�Ţ�8�i6ƶ0eGb�7�ǁ���`���S�x��v�xGJŷn���q�,ۈ��z�zٗL��D0PnSQ�Js�hȸf�I�Z5UeGB�vRt�(�y`�j�    .0�k�b�̾��ӄ���k�zC����#e�TΣ�*;�|������^�e�E2�1ϰM�#m�1�^��_y�`�^���Yb�@<!�;wM��%�;׊)�b�u���青C�&G�.SoJߕ�c/؆�\%�i����5���B�R�Q��"z�W��i���څ��D^oJ#t.ܪ*�F4�a��ӎ!	;��\�s�4�pfST^cʎ�5~)��N7&Z�Z�ۂ��LPӼ�Q�Ljp'{:�D���R�9�Ÿ�;[n�Q&�qŏ���#�d�K��Y�u��,�(l������/�]�K�w��8��8�9�i*�L�fzܑ~oZc���ִ8�ASL�X+QG�I�޵�'�Ol}���(�Lc>�R�v0K�S\�r	ٲp�U]� ��3�ŃL_S{�k���m?���E�$�m����L-�kR�6��%<����Y5�T�Od���NN�m�+U�/(jm�j
9���=vr�6c����7����5�I�T�j�L�-��T���4u �"��'~���F&ҎNs"���l�] ���-q0i� ���#�ur�PI/P+Ǹo��yC���NŤA-�MP��C;��Hr�ԃ4 ������Y���D�b��P�j�N�%�u��OI��X�֘�kY�Aߟf_DG���ES���+ս�Y�2.1�Z0C�K%뎔���BV�ꞛ�.;�9���i�S�[���A�t��q�4~x6c֧�+B޾�rSWcM>�^Bf�,wo�2eO�[~еL95;j�Q��y��0ɊG�=�ܑ&9�n��Z�$Ne�"�Hd# ����%��H�2�X�1ڎ,9�M�v���bk6�~��G� �
PG��)uLC�҂s���]Q��+=ܦ�	�|5��f��맚{jh<˨�5���V*�Q[�V���W��6����M5�0K
��U���Gj��P�L�
�V�SA��h̙�웣��I��l��ti��%�����-��'�_A-ת�5�̼PY�=z�%}�dڍ:M�\+�7|����ݵ�%}�4elh�IVj�I�(�k���S�m�������.[ʤ�p�P^kh`�c"m/z¼�=-�| ���U�Ea�
��m�i����ܪ~�b��ϱ逸>�rQ7�a7נO�?
��4�E�lp�޶K@��d����j��6M[����Z�;���R�4�<�����E�����*��4Z�j���Ԅ�l�LDGؖ5
���=�חy��T�`�/C��L#.j��嬥>5�3�mp	t?S�6_^#�s��r�c��H٘��T���#ϐ��2�����b�!���Ctd(asU3��F��eb�O���I^mu�����e�A&m�����\q�O�Ǝ�3n�"P}�F� �.h?�aK��ԥ�(���&fB��U���ƖdN�د�6Y)�`[*�``���W�p6v:�C���S�$�����KL7��1U�tQ�^SJ��t-��>��UCՏ����h����\�)O1f#WT
�~ڭ֡�j�uy���vR�<b��aA%l�DSe�P)1��1�?+\�����v��D��I_?�K[M~���8�� ��Z�7f/�R���?��Q�Q:R�<S��|��;y��SO�G�%y�� �[��j!�D�	�\�`%hhh�BG^N�9����P73�i�N��Tn�rO��uΘ<0�d ��1M�4�P	n'���D�򣃏�|N"��9;�u�R=^��cs�-B�&�,2A�M�R�Jc�Ȼ��\vBXj����r�`K��Y�Դ�iZ]� 0� n%��"	9Sh�*���<�ҌWR���U�ij�XAGG7r�X��M����Z��u�py��>���-�Mk�)�dJ}x��V��nn@GG7�J��U�sq[	R>i҈�)s#�rB�.W��O�� 9A�J���~��4J����F�!(��jtd�sӕ�I�X����EM�|euC"޴�e�Us}��0��PV��I#��+�������M��bn��VjYrd,���yu�63�ؒ����$YT��S�G@ʘR:]���M��-��T�Ӷ�q���=^W~�������lH^��I�,�u�k�=y�����F�BU[<Y�h�����R�%4�9�����b�?���cLq*���]2�o�v�ԍ�M�RQ*�o���$�����!1��;�Է�X5�E��!�➂��dG�X�ڰR��
�K4�T�o��&كU�\��L��nRh������D��k�Ő��qV���Q0���9�v�Ӡz�|����QK��>Da�p���c��*����u��s����)���欽�� ���ׯ.��z�|ZV+uK��vrQ����{1���:fG�u7�r���lf�'����6���D����f�L+DC��Z�qS7�x ��N��h]�5�V��Nr�� g���7��f��	�d� � H8 1S?M{���c>X��J�O�:\����iSE��3� I���^���~B���Бg��*�g��s�*&e��e0�Ic�V�ܿeU�7�\Z6�&
���/i�L���%I��l.'t1>��i��&�Z������
�X�f�f�q����N�$���FՆ␽ɠe�L������¼0�O���	��jg����>w��ׂ/��0L�c2͡��pEY��y3]�.�c��lS�����Ber������@\�kIr�0���f׶d?�n4i���E�t�ґ]ʁSe R��zX3Ш�]9�裂2!��¤�Qẩ=^PB)�����<������F GH"�������MS=6�1xj(EFn6��/���&¼�WE��+��Մ��u{������Q�8�Zۥ΍[8Pm�$�v1i�Bޔ)����8�1��W��}3�ؔ'E�x�k�Ԑ9tQ=W�����/�˒D�A'���oN!BGl��R��Aa1�bl�r@��ؒL��/�ddd������I��ɡv�Iv��)ռ���!��'�؜a�ˎ�z�]��`��1PuZ�y���eܿ�(oj)��:�q�r���H�Ǒ<SabU���+Kf=�0�5��X�n�����j β�H�P%c(]LU'K��$CAlF\P������ǡK���s���J��j'Vy�y
��k��� �~'ȃމ`��x����ǯ��0h
��(W�W��q���S��U~�W:��maf�ˊ�z�%��S[�T�(�"� �ݎ��cC�D��S�I��DI؄����vqg�ޚ�5J�����}���~w�[oW��(�2���8�\I�*�C�����7Wj�K7W�M��)��o)��wk����&��4}��E����Y�f��o|�Q�Y�da��������Y��"c"���Z�C_�\�4-��H9T���*@|����yPܧ� 5�۸��n��ׯLn%b���5�Y��L^��L� �f靖E�ښT=Oxϛ�t�Cv}�����Ӫ����oj1C_.dW '��ܪh�:�����#��a�R��9
��´iG�y���;�J��r���9W6�x;9©OZg�KdT>�DT��<��T�.��M���H���'��S�SkK��y�9r����/F�/Y,������=��ܸ+�5�b%}�dt2oEL�Z�ω�u{$I[ �A����u����� P�oֿ�:$Ed�Y���p-d{yc0߫���Ȝ��%b�ǕԳ'�WWG�k7ͮQU�*��d1�T�N����ƙ�S. |e��cy��B�$j*�/��8�~�k�����aP�,d&�/6�-��Q���K�,�\=�~E��r���i���k#CV�j[�\�k Z)yJ�2�P��4h����].��$��x]�*��Z�K�嶖�x�
_�*�m<�D�&�L��3�7�d�.�8�
��s<��t�f��*�D��م���h+�C���xP�?z'��+q������>H%����7���"�y1�1�S��5&�9��E/\X���2�r�A��ww��)sx�ȳf���P��d�SoKN\I���P�8�`ȳ�B�6�MZ`ܶ4y	S��@������>��k�������{����R�xP���H{    b�]�hiS��p�=`_�1w��.����f:ё�id�K4;��/E��&f�zqe��Z���!�t1a$AꊾK�A����}1e�o;SA_V\�ח
<&T@I�&9��Y�Hd)���U̝l�y�B��o��+��l"�\z��r���p�m�q	]-e�OPd��ѵ�&��4�3�؂����H_&MףE2�j�5U'�SC�V;u�ʰ�b*X%�]����MO���/���-��.����B�b�y�ijOVwd�:|�EA�rdl���4��&&�w�4���DWu �5�KS��1n����R���sM���U�D\��$�>�~���
�d��N��0�d��{��Q�d�P��3��	j�jQ�b��@�T�'M�S�"U�e����&�j��P�Vvv��zr�.��7`&Z�s��l뛨�@�F����\I��i���	�AI�Y��DV|�q�o�]���L��Y�6���ۋ��[~�:�W@5��u���h�\�T��F����p>���7Xώ)u>����\]b�ysT�v��oiEM�L�^4+qQ�(�/7�垒X$��lg���E4#����Y1�"�Zʘ_�M"	s?�j7nL�1T��6[�MCy�65�V[W�܍y
1a,���~ƙ�m�_�b&m_������F��:'��小zp3��c�c����1���0_ǫI;�`�����uѹY��I����H��V$�N�qL�jD����������@�o����eU��1(���d�ř6?�x��G_%�������7����C̽�he�ީ�{�{u0�h���[<C��0��f���"&�FVc����ݻ]/3x�Q�l9 �}%���XU�1��bwx���k��gP��JEgWG��������w]C�U�$��}j�ّ��,&��?^<S:�F���~� v^��A��V��h�B����(xF�\F[�7�'��`k��A�����������d�M���R���Iݦ�+"���M�W�=?P���x���Y]b�Y[��������GY�ɣ�>D%Hy	:W﩮kZ���^�~����k�֜�5���2�#?-��"��z�'!~|�{�^���qE1d����:Ψ��6�Pٹu}��=��������|������N�ȶ��+��iHS$դ"�=�`�3+��'�|���d�Oի�w�?<�p��l��w7E���C}�Xɱөl	��SL.^�/��Up/�Bٽ9>��?����=��Y|��[_.&J5��;l9h�t|�/v��&x���{'vF:���݇�{��WĨ��]64���N��h���r��|��3M]�pc����������<>(�������N����kԋ�<�(�-����S{�B�no��G<�� ��zV����1��Ԏ��Y>�t��3WQ4�Z&����=�p��x�y���!<��^;�����Q?���}pN� ��������x��r<|̞��FE2�ß�E)�i�u%F�nS=ׂ����N��M����|}�?J$�K�F�-3�K��d/�+�'J���C��罝�h�ԗ�:�L�n�z��û��j���P�~EJ+
����@�i�i�H�Z�<,�p_�B菻�x{?>������NGߓ;eh���BP'1�ϻ�j�ϰDȧ∬���5����"���Vw�J�M��4�hfJ2K�5��F����gN�ٹ�!����b�7"B�����B����F|�Ą̘,2u��&�w���{_d(o�46��{�?Zh��oA~�{}������N���K:�Ns��4��ު���C������Щޝ�";Im�~V��3��|���@	����?��]�]�;Šl�X��,���"��8������˟���U�f����/B��7C�Q����`K��S�cɻ4��"�t�>2:�́�N����Mi(��`�[��6�v5NE� ������2)��ei]� �'�r#�ų�������%�LSn�=�s��L��hO'VM�(�Js ���lhp���@T�d��� �-�4:(��x�-� �k��}9�g����FgGRәB�e�g�r+��v�OD�Fv�����X������NX����'"����>W6q��'�	���[ 4�̯�繨λ�WO���<�[��.�E�0!���''z�@�����)��(,؃���Q�<��kNѓ����R�"O�b��	��L3neٟ���l���/1��s���&�H��
�h��mF<�%t���eM$��	�L~��F�������v��!�����vj����v{��ⰷn�F�`{� (��Ź	�I�E�qj?Q2M���F3�D�+���5 
ŵ�_�d ��5���]"����P�����S!1*��7ȳ[��=)�d�
�fw|�1����0���������g�
d�̹�Y�I Hϩ�଻��4�*j�u����I��sG�輍�i9��Ѿ���L鹖&P�$���o�S�;8J��i-w-�>���J{{'M\Y��D�Byu�?���Pظ"���`vn��ױ�k���h��ܠO�{52>�mKQӊ��o�|�"yݪ������n3Y`M�	TTb����������|a�~$�5A�&IOU����+�m҆f��i�}�b���<��T�E5�Ros�t��6c^���s!o��K]:��!�8����r����/�jc4^s�K����:��R�����5�I���R�$I�uԠГt��dM|�-�x�Q�/��mk�-�	��Ӊ��C�.�1~�p��F�2�~����ŧ���O��I��_�h�Y�����f�O���(�p������ZQ�^u@�-�oi�ݡŷ����Uakx��;c^mD��c��kA�dLig���i�]����jx���?x�O�q2F����_��vL��xb'!��Ӊ��>�'n�M��5���<��}��s��^׻�	�y4q���x��at��+��É��|��N��=#v�F� O��K���k�2�)C�c�4��y`s;j�yq�S�\+���Tk��Nvv1��3jJ�S�P;���WI�d�WI�#�o2K�~"3"*_�ݞ����Dp��ɚ�k�b�4�"���"dG��N�����]���i�E(2�j?UkN#vr�םF�sA'W�@�F��]��^w���^w�ۣ~x�����w���o�=;u��:�?h�;)݃ܫ�����o���|9�?�����Vy�9�%����
����7}�xx�����o_&�9��.����D�%�VQG���È�>'闿x�������K�������O��'x���7����uޯ/�!��k�0��pt�WВs�S�Ǉ���o�f_r�#�T7 -{=����	\i'��Wx��ħߍy�� �/�0o^��>�˿���*�����?�K�4�����׿�������s����}����*��CQE��=��w?�_^���?������������&�k�v��'�d�P�LᲽ{���Wc^}���Qso�����_^v��@����2'S����@M�gq�{����xe��������ǵ����marA�!����#Qk��vw㍽�I��<��#�/'���t��1�����$#_�׃�������|�=bꩉ����	��@2O�3��o�������߾�9��HL-iT�if=^h�����݊���7U�t��*��0��^��J��������7�@��?��#X�~�>w2�~�sMU�[�,z(g࿑��w��,Ԉ.p��?����(2�4��e��.'8��	�3�+�8jp{�!<촏�B}zw�v��}���8����5e�o���d{��r���SBz�_B�Pf�?�
id=,k�[���ʗ��6y��mG�E���z�������q��Ŕ/��I�JRc�����,xcz�Y�����WM�R��O�v�,���1E�'y|����)�<$?I!E-�$�luy��$������zʷD��5( ��x-�8�i    �%mzr&�K����J\�+�O���¶�|�c����ʛ���ʷ�o�{�,Q(d�pه����Z���V��2�'
�|�c/����̓T8���!)�v/�[�|�/�o�,���G]D;@� ��ΎG�����-���W�.�z~�u����^A��/J51�{�˯���>�O?���������ף��:�_"�r=�=<}P`���ޘൿ���Zm�M���&4M���Ö<����J:�X�q����@\��rUA�	}�=�m���K׻B� ��5ĕ�ٝ��1��������~���{�px�f�;�� G�A����f���{�8P�s\a3*��q��4�Դ[��&A�>*Z�UI��|�>̷���h��j00��hP��r�l#��&�1_�	�ע �U"Ѥc�,.�x-bߌFI�f�d�o{��'!]���=�ÿD�g��
3������'�~ �c+g�ζ�"&AjJ���V�s<��������������C�E�H������ �����
��r�ҍ>��Ttk���!� OfC�o;��:&�H���R��?<�7b�S{�\8��[���h��w���u�L��Y+E
�t�LAfZ/����S���"�9�JE����6�r��f�K%f���:����$}��i����H�|�{8.��h@��;
d׋�h��۾.�oz����9��c=��ŋ )� �A4���7�4��]O�&�@V��w?`I��5�h^��~'̧W�~��ߎ??����{|+Ŝ/(7��E�qE����!]���I���$�3ɜ���֊�� u�'�m�م����/G�7���=�����v���m��'�,�b�{�����p �ozmσ�cn�L��F�?�|����O?���*_����������|�?ɗ��|��qZ<6��5;$I��X̤�m[�$z܂��]܉�ګ�NX��߻���5T�����컙e�<�s�,���+��r����W��������=�r�'�T���C9&��%����7������t4��[��R}��-M(t�~������[����O��)�Q����M۰Mnw¨ǟ*�%�Ǐ������g;���%^�ǽ���9j'�v <���M���Կ糇Z������:[��/�T��
&վ��J�|�I|�_��_�Oo>���ս��/��/���o^�W|��w�k0��A���[��;`��x��|~���G����O���a�����y�������ۼ�?���W�?�ƫ�^@YeS��Mv�����ͥV���	пm �خ� 6�m�s��i���!.���7�뷿~	������ן��v|����z�����nl�̖c9;� �6�>�WV���_����f���2�j�'	L��4eC� ����?�����
�����u�	��� )>���s$v�ˊtҴCܥM��Q1I�z���-�cD~�jg��⸻�?qTJ��<��?{`��GpF�Q�g�w�)F^����:�r�ީ2w�?eV)E�0y�N	�����/���ů�oՋ��^�����O?��<3{��c�y���.>X��(�>8y�5kn�4iU�b���ٙ��1�����X�����1��B`�Q�៻)�vۀ��N�bZ"�U���6৯����)��`�5�c� �́�dV
��D�=-��p|����R�J�� �墷�ޙ�W?=ʇ�������>�w��,n���{In���x��t��b/�-����h�7u r*SE�Ƿϵ�x���͇�;����f��V���O �O4vۚ�p���0����^�-�n�p������p�J��?[ �x���V�"-�-�R�"�U��QJ����_|��^����e^g�{�,���9�$`��K`ft͜�	$w'��O�
^G3<z�
�p����7�����<p@QG�,�+���ϯ_�?���km>~��z����N������3�[~n��������q�ҁ�ܸt�ƥC�\�̏�(�����IM��Ѩ;	����p7�=y}�����7n�9p��a�9�E�g���	�\�ƛ���W$(ʙ�u�+3�asc(7�Bqc(�w�P���]�5C� f�+n��:G�d�['$��[�ō}P��ō}P����8�k[5�+ s���f���"-n����?(n����?���O�-��6�������hn�F��"-�-��6�����)�-�R�"�U�v7��Jt�j�G�;�f��g����^%�\u�фC�T�}��L<>�}��?=|�����w�����c!n�J�������z~�;ԛ����Y�+�C�v��e����j���:lw�Y�f됱��^���5��ln�;�,c�uX��P�����k�C�26[���5��l�;�,c�u���P�6�M�>ꭏ:'oS��sg�`�V/�^Xk��'��z`-��O>  ��(���n������V�UW(����ٯ��A��5j��
`V]��]���x���s��xM:7�R��Y
�<K��g)p�,��{�>0�Ig��P
�J��C)ps(]�0��b<l���e�V�g�ߕ3ڍ���};��)�7�#���/@�Q��0H;u������O�����7���Ϗ���Ѽ,fi���Ҟ�[���l��` 4���ݧ_?~�����w���s�Ҽ��A�^���1n5{f�h���y�A��h
`n���=3w���@�89N��T�5�I��JCݡ�8	�RE�7�,�V�u��%�RAi��Pw�Y�f,�V*u��el�RAi��Qw�Y�f,�����TPZɄ���ϰ9���� ��a���/�ͫTq�dP�47M�}M��f�u��i2��k2<0���M�Aq�dP�47M�M�Aq�dP�47M�M�Aq�dP�47M�M�Aq�dP�4�Z�� ���`�����K�Ų_���W,;��N|Ų_���W,;��N|Ų_���W,;��N|Ų_���W,;��N|Ų_���W,;��N|Ų_���g�e��1��Q�?���4���m�@s?���4���m�@s?���4���m�@s?���4���m�@s?���4���m�@s?���4���m�@W�7fiq�Q57�O���Ss3���L?57�O���So���這U�9|jn���ç޾���s����-��<57;O�};�����i�� %� �iq�[��yjnv�����f)"��!""�K��m	��I7�/G�qPU��,ED4K�RDD��,ED4K�RDD��,ED4K�RDD��,ED4K�RDD��,ED4O�uzx�C�26�������L�����L�����̰�:3,��^òW�l�W᫣�s��
�e��a٫`X�*��
�e��a٫`X�*��
�e��a٫`X�*��
�e��a٫`X�*��
�e��a٫`X�*X�܇e�}X�܇e�}X�܇e�}X�܇e�}X�܇e�}X�܇e�}X�܇e�}X�܇e�}X�܇e�}X�܇e�}X�܇cY-t,���e�б�:��BǲZ�XV�j�cY-t,���e�б�:��BǲZ�XV�j�cY-t,���e�б�:��B��Ҏe��c�)�XvJ;��Ҟ%��Yr�%��Yr�%��Yr�%��Yr�%��Yr�%��Yr�%��Yr�%��Yr�%��Yr�%��Yr�!���V	�:����N�W	G{�J$��U�О ��x�г'��D�=^%���:u�� s<֩���[��Nͯ'��"�u*}=�i�S��	0�Hk��^O��EZ��1���L���.a���	0���j�[���j�[���j�[���j�[���j�[���j�[���j�[���j�[���j�[���jב��	0�Hk��� s���Y���L���;�4���1X������L����L����L����&����Z�d�|�->��CD��Y�Cq����饀�ӖV����[�ō^R��%ō^R��%ō^R��%ō^R��%ō^R��%ō^R��%ō    ^R��%ō^�'�H���K�����K�����K�����K�kz�O̬졹Lz���s���L����L����L����L����L���	��ߩ+K���3�N�AW��[���[��p+�n�ZíXk�k�b��V�5܊��[��p+�n�ZíXk�k�b��V�5܊��[��p+�n�ZíXk�k�b-�i �m�p�0ܦ�i �"-�i)n���i)f��*�Rw#L�+n���+n���+n���+n���+n���+n���+n���+n���+n���+n���+n���+n���+n���+�
��&Ǎ`r�&Ǎ`r�&Ǎ`r�&Ǎ`r['�ȄI�Ё��	�
^fUǍ_r��%Ǎ_r��%Ǎ_r��%Ǎ_r��%Ǎ_r��%Ǎ_r��%Ǎ_r��%Ǎ_r��%Ǎ_r��%nS���ԥS�"�U6tw���F�zn���F�zn���:e��%����w*�Y�΀_>>{a������o�����O��Ga~?�����scQ��Yԧfrzn4��F�zn4��F�zn4��F�zn4��F�zn4��F�zn4��F�zn4��F�zn4��F�zn4��F�zn4�*qVw$[�F�n$[�:ɖ(���w
Y�!"�P0B����_�����Ͽ}|�����7�x@R��N�!����b��5�&Oˊ���3�N�!Hu�����S��l[�F����n�T����0h��3+��i����6����#�_c(��x/�h�k�mzG�;Pw�AW�h���e��Q �0/1�|]q#�7�-p#�3�m������̪"p�������xH��&19T��C�y=�0��8��G���<��W�C��
`n�V��?pϑ֟�[�Ŭ�s��ؕ=��G��"-f}�0�H�Y��:�tw��U�;ԫ��>��i�4��:�uwk�Jt�j�F����ݡ^%��5ˎ�u���Ps��`�?Լ�0�L��i#`V5m̪���Yմa-' ��i�Z3���=�Zͳ'��"-^��9{�-���=�ZU�'��"-^���1{�-��n���� K��[��K�3��8������e�V�n�x�V�n�x�V����Kz0� 3��[5�U�[5�;�:~b@ͱ�x���;���W���Cͱ�x�(�7�,u�V�ǺCͲ���L�=5�،�D���a�r"XN�ˉ0�9�NSEw�Y�f�Wt��el��\w�9�f��T�b�L�X2>�%�X2>�%�X2>�%�X2>�%�X2>�%�X2>�%�X2>�%�X2>�%�X2>�%�X2>�%�Y�H5��fY#�,k��e�T���j�5RͲF�Y�H5��fY#�,k��e�T���j�5RͲF�Y�H5��fY#�,k��e�T󬑲��,��5ˮxͲ+^����26���b�)��������a�_���a�_���a�_���a�_���a�_���a�_���a�_���a�_���a�_���a�_���a�_���a�_���a�_���e��X���e��X���e��X���e��X���e��X���e��X���e��X���e��X���e��X���e��X���e��X���e��X���e��X��������X�,'���9���c�i:���c�i:���c�i:���c�i:���c�i:���c�i:���c�i:���c�i:���c�i:���c�i:���c�i:���c�i:���g�x�,�g�x�,�g�x�,�g�x�,�g�x�,�g��i`��i`��i`��i`��i`��)˾���/%��K	,�R˾���/%��K	,�R˾���/%��K	,�R˾���/%���,g��Y{)8�\5��:�f�^G��눚�{Q3|�#j��uD��fXK��#�Q���8�\5�،#�Q���8�\5�،#�Q���8�\5�،#�Q���8�\5�،#�Qs��$K�G�d|$K�G�d|$K�G�d|$K�G�d|$K�G�d|$K�G�d|$K�G�d|$K�G�d|$K�XV�eUXV�eUXV�eUXV�eUXV�eUXV�eUXV�eUXV�eUxVX���>R`�G
,�H�a�;��:�j���"�B���������2~3����)�ՠ�;����~�`E�3������{�Jֱ�x��A/�]%��i}W�4z�J����_oi���+z����^%��	�*Iş��cСﴻ3z��w�:�ގx�*o?�y��-�^�Ǣ'��K0}3`f��JM=�x8�t���镺(z��x�ɀ��+�M����A�;�b�4���[��N�DO��EZ�H��[��NcDO��EZ�Ctx�N�� o<�z:`n��:Z=�i�#��`f��&�e��/B�,&�Bށ����-|��Ʒ���-T� ��;%��Z���iR��ōnQ��ōnQ��ōnQ���u���7h=0�H�ݢ��-�ݢ��-�ݢ��-�ݢ��-�ݢ��-�ݢ��-�^57�Ao�}x:`fW��F?hn��F?hn��:��Z���W�N���+��%Ś��������N?���Tw�4A/v�hn��F?��K��4wR�1Wl闏�^�T/�����{�����7?���_>��7�|qc$47FBsc$47FBsc$���|)�)g� ����&�ܪՆ[��p�Vn�jíZm�U��j��z��這%L�[��p�Vn�j��j��s���U��֫�O�Ϩ�Ps�XnU ˭
`�U,�*��V�ܪ �[�r�XnU ˭
`�U,�*��V�ܪ �]�[���֠f�5�Ynj�[�?�y��'dw���ͫ��!�C�сX���`�ky���Uy ���	0�� �Ś��Uy �
�{̪< ׁܼ��:�i�"b0�Hk�DM(ƿ���C���!0�H����EZ�������-�����EZ���a�"mw�x�V�_i�{���n�x�V�n�x�V�n�x�V�n�x�V�n�x�V�n�x�z5�逹EZܪ���ܪ�<J����V�R�
[�[aKq+l)n�-ŭ������V�R�
[�[aKq+l)n�-�>������7c��c�|����1p�1���?0����1p�1n>ư}��iqk~ؼ���s���5?03-n���ʹx�ه�J��Lm���-p3�n���yS[������*�=^>>���훟�<�����?�v�����������7`c3�n>�������7�[��s�|n���-p�n>��[�ŭ�i�U3�j�aW��&��э�������I�V�W���XԱe�F��ȁ�9ps#nn��͍���77r��F��ȁ�9ps#������A"`m���e��-n�Zff��͌���73r�s�%SU͗���i�=xS ��s�WI�;��V����~9�}����g����^i�I|~o>�����^�l1,l<�z:`f��*�Ww�*�Ww�W	�zC�Nɫ;ԫ�cݡfi�N�;�,U�֩�u��e��:e��P����)���z�*Yw�Y�f��˺C�26[�L�j���:��P��̀cl���Lw�9��+��t���{���Lw�9��+9�t���{ͱ����Lw�Y�f,y��|h�C�26c�s)�<�b�s)�<�b�s)�<�b�s)�<�b�s)�<�b�s���GQ�gʼ�N�A	�Ű9^g+v���u���`w�9��+�	v��c����`o�Y� +v��elƒX�o�;�,c3�4�J��ݡf���Vr#�5�،%�Y� +�1v���_ɗ�;�����C�1�Xɘ�;����,{CͲ2��Ycw�Y�f,+Æeex%��Ps��V���5�;|%��Ps��W���5�;�c~���Vw�9��+yku��el�2�^�j�;�,c3���J�[ݡf���Zɉ�7�,;�V����cY-t,���e�б�:��BǲZ�XV�j�cY-t,���e�б�:��BǲZ�XV�j�cY-t,���e�б�:��B�s��圦c)��Y��=�z�gY���;�-'�e�>��e��yɿ���    �]����3S2�2{�W�H�n����ʒrD1#:��w$�Hޑ<�#yxG�������;��w$�Hޑ<�#yxG�������� $AH�4�i 	�@��$HI�� $AH�4�i 	� ��k��������멚���\S5�暪�5�T���j`�UVb�5U#��暪�ތYsS���͈��T��f�Ծ��SlS5қO���d��H��$#-HFZ��� iA2҂d��H��$#-HFZ��� iA2҂d��H��$#-HF*HZ(HZ(HZ(HZ(HZ(HZ(HZ(HZ(HZ(HZ(HZ(HZ(HZ(HZ(HZ(HZ(HZ(HZ(HZ(HZ(LZ���^�����̈́��l%vJ_T�辪����k9��v���W_���W�]��kmӾ�.���+�����־��٘z�������	n�e�u���]�&���nybe��$8�9�Ip�#ꩿ��i~c���yU> 8�|��r/i=�n�nŖ�����U�����t3���b�|0-�? 8$�Ǥ8O�^O�oE��?�%'���u�)������n2�JӌVLb������۪K��z_�={��_���w�ݽ���z��W����~�ЬWLr�Ipf��$�3{���QT|s�D����5��{���-�6�Ij����&���֫�����7zY�ti��F_�N�5��^? �f�bR�L�n�/�f�bb����bU��4�%�}֓�-=�Ѻ|��8��p�����Y �.����/�~��w�_����C~���{��{=�M�y/^�E��s�G�fZntB2�`XMt.2���5œ�A!3	�A�3	�9-Zpt�1�`�Ӣ��A3	�9��1���9-ZNt�1�`�Ӣ��A3	�9��9���9-ZP.��\��h�8oh�E5-$��!q���y�ȩ���/��)ravZi��"b�E�J�����+q=��_����m�07���Xi���S��JL��*-0UZ`���T���;����m���c�����4�e��֓��K��V;������/k��`��4�2ֲ�c����K�q-��|����kQ����n&�:n�-ǳ�s��ot���{ei�'�����������9��ӌ���!�����}&�n�[�=�~<�����f�f})��p�u��� �>R��+���UNC�NC�NC�NC�NC�~tT�x�4�Ec�~tV�x�4�Ec�Nc�Nc�Nc�Nc��c�����o����no�[����׽����sڐ��qڐ��qڐ��q�9-ڐ��nq�촞@p�S8݉��ٴ���Lk��6�Jk��6�Jk��6�Jk��6�Jk��T��R�JK�+-��T��R�JK�+-��T�=�f����u�,��<�Qi�xM��?�`�Ӣ�╖�WZ*^i�x���UiN����|e��L-3m�̴�2�F�L-3m�̴�2�F�L-3m�̴юN7ZH�h!q��č7ZH�h!q��č7ZH܎?^0�i�B�F�-$n��8�v��[B�G6�1�'���Nu�I�:ď�S��ÀҩF�)��A�T#�Yʦ:�S��f1�(�j�7�F�T#�Y:J���bN�S��f��f1ҩ&z3�9}�N5�^}��FH����9wS�'��_2�d~-��Z��� �kA�ׂ̯�_2�d~-��Z��� �kA�ׂ̯�_2�d~-��Z��� �kQ�7C�ׂ̯�_+2�Vd~���Z���"�kE�׊̯�_+2�Vd~���Z���"�kE�׊̯�_+2�Vd~mH�dH�dH�dH�dH�dH�dH�dH�dH�dH�dH�dH�dH�dH�dH�dH�dH�dH�dL��<a��<�HF�HF�HF�HF�HF�HF�HF�HF�HF�HF�HF�HF�HF�HF�HF�HF�HF�HF�HF�HF�HF�LF����)1����)1����)1����f�)1���J�f��Wdj_��}E����Wdj_��}E����Wdj_��}E����Wdj_��}E����Wdj_��}E����Wdj_��}E����Wdj_��}E����W%z���42�l�L�!3͆�42�l�L�!3͆�42�l�L�!3͆�42�l�L�!3͆�42�l�L�!3͆�42�l�L�!3͆�40��� �����W����y-�u�.?����y�ʻk~�y�m�7��z�e�/;��4Ůc_�h��<����ش.����t�CV��<�2	yDE}��t��zC�A��2	��Ǥ8��h���~����w����{��|�������Ϣ?ʯ_��޽y�����-�\�׿��4���d||�Uϥ�J���-Mo��\�׿��4���̣��4�#���ׂa�Ch�Gh�G>���܇����������<^0�i�0��0�|�瑂iN��y�yb:r	�i��$��bZp3	�9-^��E5N+N+N+N+N+N+N+N+N+N+N+N+N+N+N+N���������Ӯ�l�.r��ش�ش�ش�ش�ش��4o$�E5�6����$�-f�G&��ӏ3��� �`�jS��6�jS��R���W��>zw˅敱�_�f�"��q�u�8���i�-N�nqZw�Ӻ[������u�8���i�-N�nqZw�Ӻ[����G�ny�`��b��G�<���4�Si���8O=:繰���jE�U�R> �>*��T�4�Si���8O�q�J�<��y*��T�4�Sq��v��Ҏ1U�1�J;�Ti�[*�|o	�G-�K����ޣV�=��k��]6���X��r�}��ݝ�_���ϟ_߽-���wϿ�������	�R�8�N5rVd&I�yr"��S��k1�$�j���T�e^��Ru詌m��l��}�r��}�/���	!���j0���I0�ʖ�Ӎ����")�����'��:G$��d��3$��dLsZ���!�Ӝ�sdLsZ�Α]0�i�:Gv�0�E�%
R'�ͬc��`����������������������������������S5��(����9�u����q�W�� �]�����'��f}��?�w����o��I߼/��Y0�G�<o��W.S�m�7W��<�ϛ�h���a�a�a�a�a�a�a�a=:�K�4+%������?�u���+��8-Z��z�փ��DM݃(����Z�ܶՖU��o�yul��i�<Xëv�4��Z�Ԋ�.m!t	j�K��8�%�=/�j� ��F�t�1��?W�<��Լ�N5қ!����S��f1�7���N5қ���t���,�[�SU�|za�e��Y_Jk/�զ�Fe:J����Y�J����Y�J����Y�J����Y�J����g��S��ji���k������Z�2�R��Ҍ�MYi�R�6*Ui�R�6*Ui�R�6*Ui�R���R��j�ujݬ.]n
&ry�MU��P�MU��P�MU��P�MU��P�MU��P�MU��P�MU��P�M��O�k%r����-����WwϞ���_ݩ�����?�b�퍼�姟��{@3_4|�lN�U�0Q=�0�����}��c��s��4Q��*������l��Ҧ�*m��Ҧ�*m��Ҧ�*m������i=^0�i����P�j�3�3�3�3�3�3ɳ������K��<��<��<��<��<��<��<��<��<��<Ñ<ځ[;����
>�g��e�u�~ٛ�u,�����u�u�u�u�u�u�u�u�u�u�u�u�u�u�u�u�u�u�u�%n�
:��Nu�g9�j▮�Sk�Tb���9�A5�$[:Շ���s�Ho�ܠt�-�j�7CnPU�UEnP:�N5�^}xЉ�t��><��_:�Dt
0�j�:�N5қ!i���t����H�N�S��fHFtx0�j$#:F�N5ћ�K��y��><�]:�Dt�.�j�:}�N5ч��K��͐�4�X^:�Ho�d�A��ҩFz3$#:��M5��:��:��
ћU$�H2\�d�"�pE��$�I�+�W$�H2\�d�"�pC�������҇7�oH�    �>�!}xC�������Ӈ#;4�C�!;4�C�!;4�C�#YJG���d)�R:��t$K�H�ґ,�#YJG���d)�R:��t$K�H�ґ,�#YJG���d)�R��$KH�2�,e Y�@���d)�R��$KH�2�,e Y�@���d)��R�Jd)S5�y=U��S5�y=U��S5�y=U��S5�yd)e%����͈,e�Fz3"K���ތ�R�j�7c��ֲ��N�HoF��:U#�� �� ���YAf�}d�Q��GAf�}d�Q��GAf�}d�Q��GAf�}d�Q��GAf�}d�Q��GAf�}d�Q�ه y� y� y� y� y� y� y� y� y� y� y� y� y� y� y� y� y� y� y� y�"��"��"��"��"��"��"��"��"��"��"��"��"��"��"��"��"��"��"��2���T�}��d��d��d��d��d��d��d��d��d��d��d��d��d��d��d��d��d��d�d�u%Nk����}U]ϫ��rZ���s���^������g��ڦ}3]���W����:NS�:6�v[p�1�$8ēeb�2	qb����L�C�W&�!�+��וHp�$��bX&�4��2	�9�ܕI0�i�@�L�iN+��/�`���E��4t4K0�`��� �I0�P��$f���f3�A33	�9-�
�I0�i��e��L�iN��.y���4�4�.�`���x�Ipf�!��1_�.�_����^���p�I0�|�0O�H�L�iF��y�f�eL3Z4�4�.�`���aZ�ZЀ�L�iN+f,]&�4����Iϟ���#��[ry?j�9�t�y?jԉ�t�C�f:�ȉ�il5�j:��#�il5�Pj:�Ho��V�Ψ�S��f�il��5��j2Ղ��t�3�j�#:ՙN5ё��L���H�Nz�SMt�Ag>өFz3$-:�N5қ!iaЉ�t���I��J�SM|^�L����:>�N5�yt�2�j���R�V�S��fH�t�2�j�7C���c��T#��� ;���b�S��f1�2өFz��㙩T7�V�F���h[}�g��W��g"���|�#�0h�O�m�m�����շѶ�6�V�F���h[}m�o�m�m�����շѶ�6�V�F���h[}m��U0�i�f&^Ӝ�М�М�М�М���/$���E�AY2	�٭�9,���V���L�av+h�J&�4�E����dLsZ�1h�J&�4�E��ƨdLsZ�Qh��"D�E�B��!
-BZ�(�Qh��"D�9-�9-�9-�9-�9-�9-�9-� ��jZgK�|�L�aϦ����ÞMA�p3	�Q��	���(@���L�iN���4�6�`�Ӣu�M��$��h�-A�l	�u���$��h�-Ak3	�9-Zg��:[��٢��U�Ӣu�(��Ei�-�� Fk0Z��� ��`�6 ����hm Fk0Z o?f�Zf�N5o?f�Zk�N5o?f�Zp�N5r�pwy��w�N5��w����'�T#�pwy�چ�M5pwy�ڋ�N5қw���5)�T#�YLOX:�Ho��N5ћIL�E>լ.�ڗ�I0��D��2	Fu�HTx�I0��D�"�L�Q]&TfLsZ�a�LfLsZ�aEfLsZ�aA[T�q� ���6�8l]����
m]����
m]����
m]����
m]����
m]����
m]�����J�( �O1�jJ��اT?�SM�S����F����ҩFf��A%V:�Ho�<�+��<���2�jVʮ��J;M���J;M���J;M���J;M���J;M���J;M���J;M�����WZ���z��k��^{���+��^i��J�WZ���z��k��^{���+��^i��J�רΏL�iN+f�{"�1��3	�9-b�F5n%��4t�������;��s�S��۾�,���槕F.�F.�F.�F.�F.�F.�F.�F.�F.�F.�'��jZ�h��R��O"���E��V#�F4Z�h��h5��jD�ՈF�W#Һ[���b���u����h�-F�n1�9-�9-�9-�9-�9-�9-�9-�9-�9-�9-�9-�9-�9-��ީ�����ʼ&r�4_�7o�W`�þ����ാ��=8���i}N�{pZ߃�����ാ��=8���i}N�{��=��\��i����lY�m�4�Ek{pZۃ�����@ܬ6U��tMͦ�D0�LcӕƦ+�MW��46]il���t���JcӕƦ+�MW��46]il��M?^0�i��t���J���ig�*�L^��ɫ�3y�v&����U�9-ڙ�J;�Wig�*�L^U��R��R��R��R��2��
���Nk��F�Nk��F�Nk��F�Nk��F�Nk��F�Nk��F�Nk��F�Nk��F�Nk��F�Nk��F�Nk��F�Nk��F�Nk��F�Nk��F�Nk�4�E�Nk���t�x��❖�wZ*�i�x��❖�wZ*ޏ��_�\�.���d����
��R�NK�;-�T��R�NK�;-�T��R�NK�;-���H^��N�:�S�Sb�өq��T�X�t���]c�v:���1�;�j�7�1��T#�YL�j:�HoӪ�N5қ�4��S��f1���T#��"�YL7k:�Ho3u1�j�7����N5қ�LbL����,�K?�j���b��ө>����R������۪�}�j��ڐ��9wՐ��كd�$C� �ɐ=H��A2d�!{�Y_;��vd}�Ǩ��s���pG�׎��Y_;��vd}���ڑ��#�kG�׎���Y_#��8�#��8�#��T$-�HZX���"iaE���I+�V$-�HZX���"iaE���I+�6d}ݐ�uC��Y_7d}ݐ�uC��Y_7d}ݐ�uC��Y_7d}ݐ�u��t���렍��T3?�Ě+hA`:�Ě+hU`:�Ě+hi`:�Ho����v�S��f�L3h�`:�Ho�<�a0�j�	��<ё[N:r�IGn9��-'��#��t䖓��r��͐[N:r�IGn9��-'٫0��
٫0��
٫0��
٫0��
٫0��
٫0��
٫0��
٫0��
٫0��
٫0��
٫0��
٫0��
٫0��
٫0��
٫0�͐�
٫0��
e%��S5�7|�f�����X}L���c�VS5������GY���T��f��~�Fz3bj?U#�1����ތ��O�HoFL��j�7#��S5ћ$.H2\�d� �pA��$�I��$.H2\�d� �pA��$�I��$.H2\�d� �pA��$ÅI��繦j�7#�皪�ތx�k�Fz3Ez3�y���͐�
q��T��f��f��f��f��fF�f��7d�� ���o&�~3A��	��L��f��7d�� ���o&�~3A��	��L��f��7d�� ���o&�~3A��	��L��f��7d�� ���o&�~3Q�7C��	��L���"�kE�׊̯�_+2�Vd~���Z���"�kE�׊̯�_+2�Vd~���Z���"�kE�׊̯���������������������J�qQ�����W;�崮����_}�"�_yw��2��M�f�Xo��l�e�u���ul�K���;�Ip��$8Ąf�?3	������L�Cg&�!^3����t����e=�V�ܗr�h���n0�h�ı�ӌVL�I0�h�D��ӌVL��HpL���F�ԝhٺ����`�ӊ9!�G0/P����ڠ����*��9���*��鐙�*������*��I��Ӝ���~�$��h�6h�c&�4�Ec�A�	>:�}�`�Ӣ�Z�1��j�����)S9�:�nZ�7S���H��0����I0����I0��}�H0��<�$��N��iN��j��e��8-�f�T���M���=�X^:ռ�=�^:�!�D:ռ�=�^:�ȱ�q�=��^:�Ho�ڣ��S��f�q�=�t_6��q�=�_:�Do&��lA�ҩ&��p�T}xС�t��><�x\:�DtP.�j�7C2Ҡss�T#����K��͐�4�T]6�HFt�.�j�7:s�N5�^}x��t��><�    0Z:�Dt,-�j�:��N5қ!i�y�t����H����R=�+"mE䠭�����"r�VDڊ�A[9h+"mE��VD>^0�i�f�ڎ�A�9h;"mG������#r���A�uj�/�l]���9-���`������������僶�v���b�A[l;h�mm���-��Ŷ���v���b�A[l;h�mm���-��Ŷ���v���b�A[l;h�mm��U0�i�&�LB��0�N&��a9��o3-oA-���ԃ��zЖS�r�A[N=h˩m9��-���ԃ��zЖS��j9��LsZ��Eiy�����-J�[���(-oQZޢ���7"pD�^J��9"�7�yDMbJ��7�yD�dJ�9�8�yD�hJ��̀�G�Ħt���L�C��6�ZhC��6�ZhC��6�ZhC��6�ZhC��6�ZhC��6�ZhC��6�ZhC��6�ZhC��6�ZhC��6�Z��~�4���q��j����j�����]0�i	�i)�i)�i)�i)�i)�i)�i)�i��6)|��2�˺�OYO2��m��:��e���*�SMlOZ��N5��HТ�l��GG�V.�S�4gȣ#AҩFz3�ё���T#�YLE6�1��T#�YLWE:�Ho�_�N5қ�tZ�S��f1=�T+�0��3+�0��3+�0��3+�0��3+�0��3+�0��3+�0��3+�0��3+�0��3��L�=��=�Z�3	�=����3	�=����3	�=�i�#�]9�`�jR�$��h�#�+9�`�Ӣa��6�L�iN�5�M����&6�jT�V��L#yA�ٙ����H��H��H��H��H��H��H��H��H��H��H��H��H��H��H��H�g���6�m�E�`�I򩚆.��.��.��A��o.ѥ�m�0?m4ri4ri4ri4ri4ri4ri4ri4ri4ri4ri4ri4ri4ri4ri4ri���ׂiN��GvLsZ�=��`��"��j�L��8�6��2�j�z��6�t��㊑����2өF6� W?5i�S��f��OA=��T#W?uo�S��f��O�\����O�\����OA��T�\A���T�\A]��T�\A���T�BP�z6���'��=�j�7C&>A-��T#�2�	�pO��͐�OP�{:�Ho�L|���өFz3d���N5қ	қ	қ)қ)қ)қ)қ)қ)қ)ћ9�/ő})��Kqd_�#�Rٗ�ȾG��8�/ő})��Kqd_�#�Rٗ�ȾG��8�/ő})��Kqd_JEr���f��*��U$7�HnV�ܬ"�YEr���f��*��U$7�HnV�ܬ"�YEr���f��*��!�s5$-lHZؐ��!iaC���I�6$-lHZؐ��!iaC���I�6$-lHZؐ��!iac�B������Ԑӟr�SG� �td
Б)@G� �td
Б)@G� �td
Б)@G� �td
Б)@G� �td
Б)@G� �td
Б)@g� �e]/�����}U]ϫ��rZ���s���^������g��ڦ}3]���W����:NS�:6�v[p�c+���'V&�!�L�C�S��<�2	!��@�L�CxA"�11N&�4��dLsZ1�M&�4��dLsZ1�L&�0����W�4�%4�%4�%4�%4�%4�%4�%4�%4�%4�%4�%4��ky���UJ� �VA(��PZ��
Bi��*�UJ� �VA(���E�J�ƕ�+-WZ4��h\iѸƜW�$��bN)fLsZ1g3	�9����Ӝ�М�М�Ҝ�Ҝ�n$�U5-7Z*n�T�h���Rq���FKō��-7Z*n�T�h���Rq���rWմ�i%��J&��LN+��V29�drZ�䴒�i%��J&��L�+�h��Nk$vZ#�����H�Fb�5;�������e���ԣ��1��g�S�m���X��@�g_���/쇗����}+���߾�7����=���o+�V��4|[i����m���J÷��oq��iUT�UQ��U���J�F+���K�+Xw�E�-E? V25Z��h%S��L�V25Z��p%���:^���:^��%Dn:��.������T���t���cg:�ȁ>1�3�jdw[�M���b:����i�N����$&cN���H�V=��r�µ��^^\�G�&:Ҡ���Ti���t���4ht6�HZ�:�j�7C�BA�BA�BA�BA�BA�BA�BA�BEr3Er3=7��U��"��"��"��"��"��"��"��"�Y�"�t�����Ã���SM��A�ҩ&��]k�T}x�ֵt��ތ�Ñ�u�&�t����_2�6d~m��ڐ��!O�X���t���L��L��L��L��L��L��L��L��L��L��L��L��L��L��L��L��̑�W��rd�#;��y���+Gv^9��ʑ�W��rd�#;��y���+Gv^9��ʑ�W��rd�UE��$HI�*� U$A�H�T��"	RE��$HI�*� U$A�H�T��"	RE��$H�I��g�*��^C���I�6$-lHZؐ��!iaC���I�6$-lHZؐ��!ia�² U����}U]ϫ��rZ���s���^������g��ڦ}3]���W����:NS�:6�v[p��:����t&�!��L�C�Ι�<�3	y&g�J2	�$�� ��<��,~*�����? ��b�H&�4�C2	�9��I0�iŴI��c��4�#4�#G�<j��/��e����q�q�q�q�q9:�y�`�Ӣq�q�q�q�q�q�q���p����$��b��eLsZ1���>D���"�R��D��G[Y�Z?��+��������>�48�48�48�48�48�48�48�48�48�48�48�48�48�48�48�48�48͛�pQM#yF�5�4�4�4�4�4�4�4�4�4�4�4�4�4�4�4�4�4�4�������7�����������p�:��c�o.��m����il�il�il�il�il�S��r�ܙֱ�l����J��S���ܞ��46�46�46�46�46�46�46�46�46�46�46�86M���۪vQM���+�ף����Ո�F�+��W��4_i4����O"��h4��h|���J����4~���Jۊo�}(3�4_i4��h|������t�2�{+��/�l�cj�t�C��ҩF�)өF�g�)"өF�1�d:�HoSXfSS]�S��f1]_�T#�YL�W:�Ho&Ho&Do&1�T�w���]�ֶe���%jbJ&���]���L�Q�D%X��%*��$��X��TeLsZ�FG�J�2	�9-V��DEQ�Ӝk�DeO�ӜVL�I0�i��Ly�$*XKt�i��&�LB��0	-`Z�$��Ih�=`�εg9����z[0�i�&�LB��0	-`Z������Tt�,c}�4~u����x��W_~����7��}�������;Ac?Jc?Jc?Jc?Jc?Jc?Jc?Jc?Jc?Jc?zt��x�4�Ec?Jc?Jc?Jc?Jc?�c?��b�5�L�4�g4�g4�g4�g4�g4�g4�g4�g4�g4�g4�g4�g4�g4�g4�g4�g4�g4�g4�g4�Gi;U�H��H��H��H��H��H��H��H��H��H��H��H��H��H��H��H��Hq��TM����C�����v�Vl)�@�g����UY�y)o_���n�^�߾���{ {>W	�4Ri$��HH���J#!�FB*��T	�4Ri$��HH����#!G�i�L�G4�m��g��@hM���Ti�Oj��'���h�]RY�"K���'X�I֝b�mY�^�͢�=9����[���iʚ?&S����^Ok�/ݬ.�ݜ��h|����`�wW���F���[j��o�}l��O��K]m����m��+���w�t,V�?ze�Ho����~�������~��o߽����˫/������|���F����7�o4<�hx��?^0�i� }����?�����t�՝Fo;��v��4z�i����mOMo�D0���4V�i���Xm���Nc���j;��v��4V�i���Xm���Nc��ji�ԝ�M��B    J���Nu��)��gT:�!�t����b�@:�ȱd1� �jd�D0Ȧ:��S��f1� �j�7�!	�TkL�[>լ��]0
��Q(t�B��`
�<t����}���M����B�]0*t��B�)��޷�9-V{�.��X�}�`��b���iN��޷�9-V{�.��X�}��ұZ��Z��Z��Z��Z��ڠ����*���(��*��6�L�iN��j���3	�9-�j��$洈�՚��6ӰG�ov&����Ұ�Ұ�Ұ�Ұ�Ұ�Ұ�Ұ�Ұ�Ұ�Ұ���jD�ՈF��V#�F4Z�h��h5��jD�ՈF��V#�F4\�H�ƍ�[�h��v��$뾯��E��l��^����ziF�v��h���v��8s���K��K��K��K��K��K��K��K��K��K��K��K��K��K��K��KOM.�D0�i�Х�Х��%q��TMC���.+]V��4tYi���e���JC���.+]֣��~^��[i��,���`�Ӣ��JC���.+]V��4tYi���%mv|���I0�i	�i	�i���tyK;z�2�b=�ԻnڶbS�����ٯo^�����������˗V~���oֻ_���U�E0��4Z�hL�E0����G0��J����[�vۊ��FK`�����-Z�h	L�%0���4Z�h	L�%0���4Z�h	L�%0���4Zb;�%0��	̣o3-}����҇NK:-}���=}x�`Z��i�C������eb�\'�֩yv���ݳ�^��o����|���_��?��N�~�>|hދHtZ �i�D���	$l�IY�n�=z��9�6[�f7G�wZ�iyD����GtZ�qy�|.F��v[��|,���lL�I�:Ā�S�Bҩ�"�T#�V�Пt���2c8P:��6��]��T��A[�ө&>������}�����إ�워����WM|^m�O����&������T#�����O��͐,%h�}:�Ho�d)1�5�T��ؤS��f1�6�T��#ɰ#ɰ#ɰ#ɰ��窉Շ#ɰ#ɰ#ɰ#ɰ#ɰ#ɰ#ɰ#ɰ#ɰ#ɰ#ɰ#ɰ#ɰ#ɰ#�pE���I�1h�<�vT���O�^������m��H\X���"qaE�ą�+V$.�H\X���"qaE�ą��v�SM|^��M����Z��N5�y�F7�j���R���S��fH��e7�j�7C������T�Y�>�t��O��Ux�T�\AK�ҩ&>����SM�
A��FR���y�T#��*��K��͐T�3��Cc Y�@���d)�R��$KH�2�,e Y�@���d)�R��$KH�2�,e Y�`��p���3��qrl�@�M��f�fe%� S5��l�~�M��o����m�4�j`�9U+ͩXi���L�HoFL�j�7#� S5ћd�Y��fAV�Yid�Y��fAV�Yid�Y��fAV�Yid�Y��faV��~��HoF�7����f�~�����IIIIIIIIIIIIIIIIIIII�I��S�j�7#�N����L�)�"S E� �L�(2Pd
��@�)�"S E� �L�(2Pd
��@�)�"S E� �L����������������������"{��3lȞaC�� �� �� ����#�=G�{�L���92�sd���tϑ�#�=G�{�L���92�sd���tϑ�#�=G�{�L���92�sd�W���"iE2Ҋd��H+��V$#�HFZ���"iE2Ҋd��H+��V$#�HFZ���"iE2Ҋd��H+��"O@4$nH2ܐd�!�pC��$�I��7$nH2ܐd�!�pC��$�I��7$nH2ܐd�!�pC��$ÍI���qr6NC�����#����>:2����#����>:2����#����>:2����#����>:2����#����>:2����#����>:2����#����>2���c ����>2���c ����>2���c ����>2���c ����>2���c ����>2���c ����>3�@�Dȝ��y(қ)қ)Л�J̯�j����çj����>|����çj�����_Gz3b~=�:қ���בތ�_Ͽ��f��z�u�7#��S5қ���͈��T��f��Z
2�(�� ����>
2�(�� ����>
2�(�� ����>
2�(�� ����>
2�(�� ����>
2�(��C��T��T��T��T��T��T��T��T��T��T��T��T��T��T��T��T��T��T��T��T��T���.��pA���?|���%���_TY�۽�WwϾ{�����o��>��������<x'bދ�;Qϫ��i]��Ͻ��y�ʻk~�y�m�7��z�e�/;��4߀ul�K�歏��2	y�e� �$8�ٕIp�c+����IpH�$8�!$�dLsZ1�N&�4��dLsZ1�M&�4��dLsZ1AM&�4��dLsZ1�L&�4��dLsZ1f2	�9-�9-^W�E5-3:6�I0�_��$�/���e�A�N𥝩L����y_����Ww�~����_������/�ǟ~���7_<|`�3�\&�4�E�Q���eL3_�5�\&�4�E�Q��%L�Q�����;�uQM��J��J��J��J��J��J��zt��x�0C�4t�4t�4t�4t�4t�4t�4t�4t�4t�4t�4t�8tI;�� J;�� ��kմ��h�����F��?-~0Z�`����?<����/�������i�D�h���	�%FK$��H-�0Z"aGO$.^d^���mE�Vo�F$�H-�0Z a�@�6��h#��6��h#�x[�.�i���BE���N�*:-TtZ��P�i��=T|�`�Ӣ%�NK�� :-AtZ���i	�=A|�`Z���i��"D�E�N��!:-B����J�*-`������J�*-`������J�*-`������J�*-`���������4h4
�h��(@�Q�F� ���z�v�5��m7�6h4�h��� �ݧT��M׹ʶ��域`�4�K�ۂiF�4hG� }ǺY�����^�|�篿�����ꇟ~{{W^=|��i��xrӡ�NC]���:uu��4��i��u=^0�nu��4��S��'LsZ4��i���XW?:�z�`�Ӣ���[�՝v���NTwډ�N;Q�i'��gs�ڷ�6�e��zaF+�ud˗b O:�!�#���Nu�I���8���S�ÁҩFv���l�c�P:�Ho�ҩFz�T�N5қ�@�t���,f�W:�Ho�Do&1���T���t��SM�f��V(���$��c��4�i
2�d�)�LS��� 3MAf���4�i
2�d�)�LS��P��P��P��P��P��P��P��P��P��P��P��P��P��P��P��P��P��P��P��P��yB�' yB�' �20d
`���)�!S C� �L�20d
`���)�!S C� �L�20d
`���)�!S C� �L�� � r�!� r�)ћ92�td���Lӑ��#3MGf���4�i:2�td���Lӑ��#3MGf���4�i:2�td���Lӑ��#3MGf���4�i:2�td���Lӑ�fE�\�sUd�U�9WE�\�sUd�U�9WE�\�sUd�U�9WE�\�sUd�U�9WC��Y_7d}ݐ�uC��Y_7d}ݐ�uC��Y_7d}ݐ�uC��Y_7d}ݘ�5���!�H���#	RG��$HI�:� u$A�H�ԑ�#	RG��$HI�:� u$A�H�ԑ�#	RG��$Hȓ�y�#O"w�I�<�ܑ'�;�$rG�D�ȓ�8]Y�r�t����y�\^�nŗ�?��c�_e��j絜�u������W��+��Y浶i�L����������ul�K���g�Ip�)�$8ďeb�2	qa��X�L�C\H"�1�]&�    4��eL31!]&�4�Re��x�+�˹�K�<�aYF��4�JeL�1QT&�4�@eL�1�S&�4�6eLsZJsZJsZJsZzp��w���]�T��9-�9-�9-�9-�9-�9-^��E5��Gh�<B��Z'��:y���#1#2	��-��.	�uIh�KBk]Z��Z��ֺ$1S2	�9�ԽZO"��h�ZBk�Z��К��֬%�f-�5k	�YKh�ZBk��ͱ���E�J��!*-BTZ���Qi��"D�E�z���H,vi$���e�-��h��"D�E�J��!*-BTZ���Qi��"D�E�J��!*-BTZ�ț�QM�[���-o1Z�b���hy�����-F�[��y��Ӝ-o1Z�b���hy�����-F�[���-o1Z�b�������j��j��j��j��j��j��j��j��j=5���ڵ����	+m_��V0]/���m[ˢ�\�Ϣ�Z��Z��Z��Z��Z��Z��Z��Z��Z��ZǑZZg��:���G����*��������;��׾�Y�?�ï�}�o�<��o^������<u�۫7���
�O�UZ�Vi[�El��UZ�Vi[�El��UZ�VSGlO"��h![��l��UZ�Vi![M����Щ��ֽ�c���޿��Uۼ����T�J���C�+��w���;|[0-d�����B�J�*-d����=d{���N�IӜm�MU��R��R��R��R��R��R��R��R��R��
��N׬�h�Z�֬�h�Z�֬�h�Z�֬�h�Z�֬�h�Z�֬�h�Z�֬�h�Z�֬�h�Z�֬�R7k=�`��:z�֣Ӛ��Y��n�*��k��؏�oV�OLsZ�f���R:�i4��h|���N��F�;��w��4�i4��h|���N��F���i���K�T7���m���V�����.����(�t�iF��;�w��4�S����eJ����O3Z!��t�v�P����AC���j��4T;h�v�P����AC���jՎ�����9-�4V;h�v�X���ڑ��>�`�Ӣ5NZ���M��)��6�r��\� �l�ZYa���rS0�}L�,�1����rS0�}L�4�K��
KԦ`�ӂ%jS0�i��)��>�D�w��/��5jS0�i��)��`��LsZ�Dm
�9-X�6ÜV�WV��!�{+��/�t�C��ҩ�K�:�jL���&�~L�y 7��L���bj�t���,��L���b:7өFz���t���L��,fqy:�Ho��<�j�7�Yf�N5қŬ5O���b�gSmHofHofHofDo&1��ҩ&��K�1�t��Շ��K��X}H�ѺOU��>ǲ���u��j�=�l�H���C�s�9�@�-��l�dK� [*�R)ȖJA�T
��R�-��l�dK� [*�R)ȖJA�T
��R�-��l�dK�"�=E�{����)2�Sd��ǈ��s�D���tO��"�=E�{�L���)2�Sd���tO��"�=E�{�L���)2�Sd���tO��"�=E�{�L���2�3d�g�tϐ�!�=C�{�L���2�3d�g�tϐ�!�=C�{�L���2�3d�g�tϐ�!�=C�{�$Î$Î$Î$Î$Î$Î$Î$Î$Î$Î$Î$Î$Î$Î$Î$Î$Î$Î$Î$Î$Î$��$��s�<���s�<���s�<���f�E��\���E��\���E�١Q�١Q�١Q�١Q�١Q�١Q�١Q�١Q�١Q�١Q�١Q�١Q�١Q�١Q�١Q�͐١Q�١Q���f��f��f��f��f��f��f��f��fF�f�=ېݳ�=ېݳ�=ېݳ�=ېݳ�=ېݳ�=ېݳ�=ېݳ�=ېݳ�=ېݳ�=ېݳ�=ېݳ�=ېݳ�=ېݳ�}td�ё�GGf�}td�ё�GGf�}td�ё�GGf�}td�ё�GGf�}td�ё�GGf�}td�ё�GGf�}tf���ґ�C2�Ȝk s��̹2�Ȝk s��̹2�Ȝk s��̹2�Ȝk s��̹2�Ȝk s��̹2�Ȝk s��̹2�Ȝk s�A̹�J̹�j��z�>��j��z�>��j��z�>��j���R�J̹�j�7#�\S5қs���͈9�T��fĜk�Fz3b�5U#�1皪�ތ�sM�<of���}�]T�辪����k9��v���W_���W�]��kmӾ�.���+�|�y�)v���F�-8�<�����L�C��3	���$8���$8���$8�|�$8�rN$8&��$��bb�L�iN+&��$��b"�L�aN��3/�i�Ch�Ch�Ch�Ch�Ch�Ch�Ch؃+^T�܇�܇�܇�܇�܇�܇�܇��-t��3]T���������������nI�\�Si���2�L�[����0[�f��f��f��f��f��f��f�����#�����5�i�iM�NkwZӸӚƝ�4�1�m2	�9���6�ӜV�$�L�iN+f~M&�4���!�`�ӊ�ɐI0�i)�i)�i)�i)�i���Z�L�t�C���T���ZԈ�t���0�;,jbQ:��s0��5�(�j�7���>�T��U�u����e�׵�rsY�G�&>����SM|r��I��H���SM|^�I������N5�y4$'�j��M�I���R��ȤSMt)Aeҩ&����2�T]JД�l���
A�f����N5ёbI���H�F��SMt�A�Yҩ&:Ҡ1-�T#i���t����=���٠!.�T#��{6h�K:�Ho3�%�j�7���N5қ�L|I���bf��S��f1S`ҩFz3Ez���0�T#�Y�x�T�}-�������b���K��\���Uo�{�
�{�
ּ
ּ
ּ
ּ
)�>R�]<���vZ��c[�R���k^���\�i=^0�i���\ӜiY�U0�i���\gvZO"��H�b��iN��,�*��NKʅҮ'Y7���R�}u���K����_��˛/�Z��;y���W�~��÷��H�c��i����cd�_)�4���fe1������_���]���ʷ���q-?�Z�|��=��1�F��`��1�F��`�#m��9]���W�ѷ��+4�;�`X4�;���U�^�䵜�Nk�Jۤ-c��WΫc;�|Y�V_�n68���t�a%��z$u���%��%�ڶ�"��ϰ��O��K��K�LsZG��~��t珮�j��2[��V
����&�I0���!�I0����I0������c=���������U��p��IgvZ�_Z�E��5t�Du�埿�z.멌�l3_����>z��;LsZ�fc�5+��Xi��z�f��ic;�^}�e�v�� �}��Wo_���7���痯��R�<|h�����c������?B0�|Ѻ���m̛�wQM�`�V%f{6-H�$�l
Z��I0-C�$F�V fLsZ��h��"�E0F�`����#���9-Z�b����P㢚�.�vjg3�A��3	���ٙ�u���L�a�:hWv&�4�EC�A˱3	�9-���[���ŕVWZQ\iEq�ŕVWZQ\iEq�ŕVWZQ\iEq�ŕVW\QL�穴~�J�穴~�J�穴~�z����uF��e^�e��^�Ѣ����ӕv|:�f��[-ni�����F�[-ni�����F�[-ni�����F�[-ni�����F�[BlG:
�=���1 �T���t��nc�H:��A�1�$�j�!����t���,�9$���t���,�W$�j�7�iI���bHҩFz3Az3Az3Ez3Ez3Ez3Ez3Ez3Ez3%z3��u�T�6��\ �������R�ǫf����yr�ʩ�ݟ��V�[��w�����v�I0��D��_e���f�L�Q=��b�I�����x�S雌m��������F��7�s1Z��1��E���d��JT#M&�4�u�*�/�q���V����8���|k��Du��L�a$
�&��G������-槅.�.�.�.�    .�.�����iN�!��Z0�i�ȥ�ȥ�\>^0�i�Х�Х����gA�`��b�b��h��h��h��h��h��h��h��h��h��h��h��h��h��h��h��h��h��h��h<q �TM�<F�<F�<F�<F�<F�<F�<F�<F�<F�<F�<F�<F�<F�<F�<F�<F�<��<��K�u]���h]�F뺴�w]^Ƙ���e��j��m�7Z��[��=����������[�⇯^���?>����W/�=|h�+f�B&���W��5�}�����������W7�ܜg1�_����	�Ì�!��g���ܴun�SY�����MH�GɯC��T�6��"��A�N���!�د�W����,�=���p����~����J�z���ϟ߽����<|^c<����x�`XI���i���rs�.�zt���Ls`����av���V�٭J�[�f�*�n��+�M���+�M���+�M�������y���m���e���~��~�/�7�ݽ�����~�B��W����u.VZ�bȇ:���S�YΦ:��J�����s��=V1�U:���1eV:��2��J���b�l�c��t���,��X:�Host,�j�7������y�����9�mK��_�={���Ww��_�~����_��Ï�����7��4���:���M���~�eY�G�&~���T��קy�����YǦe��Ѫ��gИ�t���g���l���@���t���mH����?W��f�` h;@:�Ho�(2Pd0��`@��@�0�t���,f�\2�A��ҩ&>��V�SM|^-7J����Zs�N�!����}�}���~��TYJ�֣t����s-AJ��͐9W�N�t����s�Hʦ�s-KJ���͂�اSͼ�D4�>�j��f�N5ч͵O�����H�H�H�H�H�H�H�H�H�H�H�H��H�gy��gy��gy�c6�gS�Ho�s>�j�7S�7S�7���N5�s��5�j��Xs��M��Xs-�M��Xs��M����ґ6�#mHGڐ��!iC:҆t��Hґ6�#mLG���Hޑ>�#}xG�������;҇w��Hޑ>�#}xg�p�)��<�֑��:�[G�b��Sly��#O�u�)��<�֑��:�[G�b��Sl�}d�1���@f�}d�1���@f�}d�1���@f�}d�1���@f�}d�1���@f�}b�QV"#�����çj����>|����çj����>��DF:U#���N�HoFd�S5қ�T��fDF:U#���N�Ho�d�����͈��S5ћd�Q��GAf�}d�Q��GAf�}d�Q��GAf�}d�Q��GAf�}d�Q��GAf�$H�$H�$H�$H�$H�$H�$H�$H�$H�$H�$H�$H�$H�$H�$H�$H�$H�$H�$H��YAv�
�{V�ݳ��d�� �g�}(2�Pd����C�ه"�Ef��>�}(2�Pd����C�ه"�=F�Q�����j��m�7�K��Ѳ��~(2�Pd����C��"�E�
?�J'qQ���z��絜�u������W��+��Yd���7��z�e�/;��4Ůc_�h��|�e�-�Ip�X&�!�e&�!%e&�!�d&�!�d&�!5d"�1l?�`�ӊ!��ӜV��$��b�}&�4���3	�9�P�I0�i���L�iN+�gLsZ1�2	�9-�9-�9-�9-�9-�9-^��E5-�
�I0����I0��� �I0�����I0�����I0�i�R��~�Ӝ-��I0�i�R�~��R�}�Ӝ-�ӗI0�i�RqމӋjZ�4�0�`س)h�`&��gS���L�a h�`&�0
4a0�`�Ӣ�-A#3	�9-Z�4B0�`�Ӣ�-�[F�<F�<F�<F�<F�<F�<F�<F�<F�<F�<F�<F�<F�<F�<F�<F�<F�<��<��Z������h}�F�5Z_���v\_8���}�r[/�hцMb���/9-_rZ��|�i����%?z�4�G��ӧ�6�_�@0�aL˗��/9-_rZ��|�i����%��KN˗��/9-_rZ��|�i����%��KN˗�����Ӝ-`
����J�*-`������J������=�iS�L�0UZ�TiS�L�0UZ�TiS�L�0UZ�TiS�L�0UZ��PJ�N7�n48�hp���t���F�Ӎ�N7�n48�hp���t���F�Ӎ�N7�n48�hp���t��醃Ӵ��v���N?4��&4�%4��0���.3��̴�2�N�L;-3��̴�2�N�;-B���"�N�;-B���"�N�{����.�w�`�l������/�]�M����{:-@� ���N;-@��w��z^۩��M�eY����,Z~�i�a��������uv�w>�/�u,��m�4�E�C�o��p���A�-?��p���A�������Z`:h�����Z`:h���#u`�$�YN+�vd�<1�#���Nu�I�:Ċ�S�G���f�N5��Lb�u:��o3����TW�	rƭ O�
2[d�)�JS��T��T��T��T��T��T��T��T��T��T���}(2�Pd����Cc2ҩFz��֌t���,�\{:�Hos�=�j�7�9�N5ћ20d
`���)�!S C� �L�8�y���#�׎|^;�y���#��}^ɰ#S{G���L���;2��Ț�"k����*��Ț�"k����*��Ț�"k����*��Ț�"k������jȚ�!k������jȚ�!k������jȚ�!k������B�N��J�#+͎�4;����J�#+͎�4;����J�#+͎�4;����J�#+�ά4�s�:rRG�A��9H9�#� u�����ԑs���$H>�<| y�@���������$D^V�#����t�:ҩ�H�j�#����t�:ҩ�H�j�#-+ёN�HoF��(��҇�/H^�>� }xA�������҇�/H.H.H.H.H.H.H.H.H.H.H.@�V"����}U]ϫ��rZ���s���^������g��ڦ}3]���W����:NS�:6�v[p�	�$8�gb�3	qݙ��L�C�v&�!6;����Hp���$��b�v&�4�3�#�`�ӊ��G0/����Q��(.�`س)(��$�l
��2	�Q���-�`��2	�9-����I0�i���L$8f�g&�4�3�3�`^_�E5��*��*��*��*��*��*��*��*��*��*��*��*��*��*��*��*��*��*��*��*��*����_�$�洄洄洄洄洄��xS�Z���t�yS�Z�I�t�CT�T�R�����T#�3�ö�s��T#�YL>�N5қ�$5�T#�YLf�MuLp�N5қ�D8�T#�YL��N5қ��:�T#�YL��N5қ�D=�T#��"��"���� �m_�$�W ����¢��6Y7kK+�/|p��M��]�}u���7/_�������W��w/�x���o�<|P��B�` �B�/���B�/���B�/���B�/���B�/����t�I0�i���JT�A&�wZ���r]ϫ��LsZ���Q�t�B���J����&2B[�#��:B[�#��:B[�#��:B[�#��:B[�#��:B[�#��:B[�#��:B[�#��:B[�#��:B[�#��:B[�#��:AD+��-��b�-��b�-��b�-��b�-��b�-��b�-��b�-�	�zHW'��?�SM� �	�N5qLPOD:��I���A-�T#�rrrP�D:�Ho����@�M5rrrP+E:�Ho���,��ɂ��,���AmC�T�ڥ4�$�'�
�6wQis5j)f&��v)��TڠI��TڠI��TڠI��TڠI��TڠI��TڠI�����|�`b���|J�ʧ��|J�ʧ��|J�ʧ��|J�ʧ��|J�ʧ��|J�ʧ��|z��|�e��ʼd�ե���_�={Q~�/�    �]�ɿ�U�^��◗����=��/̈́�Sڠ>��Sڠ>��Sڠ>��)�$�����iN+���#���|-�RNEw�k[���&�
QڰM��TڰM��TڰM��TڰM��TڰM��TڰM��TڰM��Tڰ�]0�i����-J�[���(-oQZޢ��Eiy����x���F�F�F�F�F�F�F�F���4~\_|��V֥>�����}�˗wo^|�շ��}�����o�~��y���F�z�z�zK����TEO�n>����(�4�E�F�F�F�F�F�F�F�F��$�;LY��si��Y/���|��!�N; �H�i���"	�EN�$�I8-�pZ$�G�$/��h�������:x�4�E���?8-pZ��\�Yi$��H^���J#y�F�*��Uɫ4�Wi$��H^���J#y�F�*��Uɫ4�Wi$���]�N��fc+uQ�ۂiN�F�B>��H^���F#y�F���5�k4��h$��H^���F#y�F���5�k4��h$��H^���vt��x�4��ِ��e��2�/�4�E�	򕕎Ն|oeSS&�S��Nu��X:��u�1�c:Շ��F����$ө&z3�I�ө&>����SM|^�O����şN5�yMd)A��өFz3$K	�ʟN5қ!YJА�t��ތ�Rb��l�cB�t���,&�O���b���S��f��fB�fA�ҩ&>����SM|r�7H����
�t�N5�*�<Ȧ��m?H��͐��#����T#�2�	Z��N5қ!G&>�L|��82�qd����Ǚ�� �� ��"���Y�.�t���4h�U:�DG��*�j�#�t�N5ё��ʦ��m�J��͐In�2�t�������J��͐InЪ�t�����Vd�[�In�F�t�����p�T3?�Ě+hU\:�Ě+hi\:�Ě+h}\:�Ho���A��ҩFz3$Z.�N5қ!yxЮ�l��<<h�\:�Ho����Ñ'��dSC�ljȓMy��)қ!O6ud�ۑInG&���vd�ۑInG&���vd�ۑInG&���vd�ۑInG&���vd�ۑInG&���vd�ۑInG&���vd�ۑInG&���vd�;���@�{��d�7���@�{��d�7���@�{��d�7���@�{��d�7���@�{��d�7���@�{��d�7���@�{��d�7��^_0ݻ���}U]ϫ��rZ���s���^������g��ڦ}3]���W����:NS�:6�v[p�1�$8ēeb�2	qb����L�C�W&�!�+��וHpLP�I0�i�ē�ӜVL(�I0�i� �E5�(ZQ,G/��E�������K���ZQ,��XR��Oi9�؟;S�����wm~�m�z[.�$ZI,��Xh%��Jb���B+�y?մ
Qi��B|�`�7�BTZ���+�'��V#*�FTZ���Qi5��jDZl�1'83	�9��s��Ü�L�E5lllllllllllllb ����wEO��/.�"��%o D�:��Nu�I�98H�GRL�y�	8H�G�YL����?W��f�A��j�7��*�����*�����*�����*�����*�����*�����*���
�u��d
*�fZ�t��F�+�`C��6�H�5z���	6�Hhc��6�Hhc��6�Hhc�v�4��:���9-֑�]0�i�,�$洈{B��'�i{B��'�i{�|ڞ��.�K;�u�J[�R�$���?������?|;d[����a�6mOh���6mOh���6mOh���6mOh���6mOh���6mOh���yҁ-ش=�M�ڴ=�M�ڴ=�M�ڴ=�M�ڴ=�M�ڴ=�M�ڴ�]0�iG�Lմ�i5��jD�ՈN��V#:�FtZ���i5��jD�ՈN�W#Қ������5?8���i綈c��jɫ4�Wi$��H^���J#y�F�*��Uɫ4�Wi$��H^���J#y�F�*��Uɫ4�W?�7�@���m�4�G\54�6�j�ؠ��T#��!��ͲM�y��6h�m:�Ho�\SgeSSl�SM�fA#`?Uuy�%ޫo�b�����~�j�#�N5ё��M���H�ĦSMt�A�b��F� ���T#��͐M��͐�0h�l:�Ho������ʤ���fB�fA3�ө&��MSN��X}�UN��X}MXN��X}�ZΦI���.�S��fH24�9�j�7C�aC�aC�aC�aC�aC�aC�ac�aAz3!z��-�T�͂�e�SM�6ڜ�N5��ڡ�N5��ڦ�M52ګ�N5қ!S G� A�6ҩFz3d
�z#�j�7C� �L�82pd
��@��L��,h�R:��'W�^�t��O��K�T�\A��ҩ&R���K�T#���K�T#�2�	ZǔN5қ!���L�T#�2�	Z֔N5қ!��L|*2��ħ!S��L2h��!S��L2h��!S��L2h��!S��L2h��!S��L2h��!S��L2h��1S 乏�<�����͔��:2���L�#3͎�4;2���L�#3͎�4;2���L�#3͎�4;2���L�#3͎�4;2���L�#3͎�4;2���Ls �@2ҁd��H��$#HF:��t �@2ҁd��H��$#HF:��t �@2�Ad�e%���R�j�K���.e�����R�j�K���.e�F�"A���ތH��j�7#���͈i�Fz3"A���ތ�e7U#���n�Fz3�䐩���
2(�� S��L
2(�� S��L
2(�� S��L
2(�� S��L
2(�� S��L�����������������������������������������������������������������������������͔�͐��x�|�9�[�uY�k?�z*c��Y]����#�*��W;�崮����_}�"�_yw��2��M�f�X��͗��q�b�����F�-8��;����#����#���b#���:#���#����"����"��ޛI0�i�P�L�iN+��fLsZ1D7�`&�:U�I0��t�2�`س)�e&�0
tn2�`:-�I0�i�(@���L�3;-ٯ�����{9�e���~���E9�|^�(�K3Z8�ѕI0�h��q%�I0�h�4neL3Z1KB2	�9-�9-�9-�9-�9-�9�C�j�1��s)�UO�����"���Ht�i�i�(�L�a�2h�F&�0{46#�`����H0-1��I0�i�Ӡ��Ӝ-2��I0�i�"Ӡ��Ӝ-2�⺨��Z��Z��Z��Z��Z��Z��Z��Z��Z��Z��Z��Z��ZK�j����,�Ne
��,ky ��U�l]>��f�h��7������������������?�Z����`pp�myQ#�өy<�S���2�&2�S}����j�S��5�9�j$�n�Q��f1m�-o��&�-o��&�-o��&�-o��/o�2�u�q��\���b�B[�$��MA�@�UA�S��L� `�_���Eh�_���Eh�_���Eh�_���Eh�_���Eh�_$��'LsZ��1A�ҧR���:�x�ߓ���6��ᙯGR��3���8���K�Mmiv[/���e,�Y�B�e,�Yƒ|�q��k��5{�p}݊NIz�%=����y=�,c���a 6�Xh���6�Xh���6�8(l�Ԋ����)�t*u׭uq���	6SPh3�6SPh3�6SPh3�6SP�?S�ݴ���%��E���B�)(���B�)(���B�)(�g
>�`��:���r�#��*�/��4�����Wޙ%Iq-[��;��@���f71��E�ES�h���3T���r��3�I��X�D�����F�J��V�'��=���	�dOh%{B+�Zɞl�d�b�4U+�Z��0�i1�k*M2U�d�4�Ti���$S�I�J�L�&�*M2�_F2���~�'�4�E�L'�hAb���v��*4�UhLK~    ��_�1-�1-�.�����6�W�h^m�y����6�W�h^m�y���Ն|���V�ѭN�[�F�:�nu��[�[���j9�]e��~�f�ѭN�[G�~�h��LcZ�h�Ӣ�N�Ɖ+�'j�F4�8hq�4�iıu�x9`�F4�8hq�4�i�Aӈ��N#�Ƨl|:桔��y2�C�\Y�J�z5��:�M���먑E1�~��Z��������>Ů����,���è�O��l��z)i9|��K�K�3 r}���#+�^=���g���?����O��ܾħYІ�t��O��]m�P甃��eC�t����C��kH�A�N�"�E:�tbZF�����o���2˓�V�}Y~tGЮ�t6���1j�;hc:��v�&�t��fJ�N�t�7A��u�Hn�4S��<�C��fH3Őf�1͔���l�c�[ӡ&r�����Pi�"�t���4h�m:�DF��6j"#Zs�5�-Zx�5��!�BG���t�:�-t�[�H�Бna��t���,h�y:�Dn��<j"7�p�5���:φ�m=O��͐�Y��t�����DO��͐�Y�f�t��,%h�r:�D��m9j"K	ڻ�5��m`Ά� 5��ԐRC:H� 5��ԐRC:H�*t��Б�BG�
�*t��Б�BG�
�*t��Б�BG�
�*t��Б��@*́T��4Ri��H�9�Js ��@*́T��4Ri��H�9�J�_d~=�' ��@6�d�� 7+�#����5��O�@>Qy�D��5��O�@>QyxY��D��fD�t�Fr3�G:Q#��#���܌�N�HnF�H'j$7cz���f��f��f��f�,��}�P��>����vK�Z�����_}�"����d^k���to�ݾ�햺+���Wk��Qo�^�<���={���wRʧǏ{���o�A�!�{~d�*���"�n߽o?~x��^�����x�����~^ޔ�oA�S�^��-�j~嗱���h��!�L�C�e� �<�2q212�~p�
ɏO�:y��ת��몯E�Ko�=�^�.������#�'��C��Ԯ����Ĥ?�>����� ��~��urR�7��3�����g�O޼{y-���#���ѳ���}�O�.��it,&���F�b�L�it,&��83��1�N&��^��D�sr��+�(%�j��������	*��	z���X��1j��B38�fp
����)�����: ������>��w���}������=�������}p�%'�4�4�4�4�4�4�4�4�['`��ɺ�8�r�4�E��+-<VZx���Xi���c���J�5�@`&�4�s0`ܩ�j�o[w㏹Ӽz�tu���~�����?���T?������O��<������@�[Y�F�u�{i���_qY�.y�O�8�NFs���͝7�;o4w�hv���AۯJ]�����z���*cߗ��q'�4��['ؗ�̧�0�P;�P��	�� r9
�)Ǿ�y���'�G���|Xǻq:}�zU��eU��?}�0:�4:����� ��,ڰ���n]0���|��m]�U۟���.���t�bG����/��i?��r�[�S݉]���Z�?Q��0�T�.�.�q�t9�����S�	�JL�&�*N0�N�ԧ�Z���Z������1-��J;Pi�*�t n�	��}��?�;y���˖�ɻ���� ���Fs���k4'��2N�O����\m��9�ĭ\=���f�C�\��V�CM,��Bʠb�t���p������t����A���P�GPwy6�H�!H�!H�!L�35�5����ϤC��f1�4�P#�Y�LM2�A�dҡ&r���2�P�Y�z�t���,h�L:�Dn�r&j�o�|&j$7C�fA�hҡFr3�o��&j$7C�fA�jҡFr���5�P#�Y���t���L��L��L��L��L��L��,hY:�DF�,j"#ZE�5��-%K���H�֓eC�Lr���C��f�$אI�!�\C&�A[ӡ&�Ã��CM��m&L��y'��U��P�u���t������v�C��fH}�L}���v䤴#'�9)��IiGNJ-N���Yyx�&�t��<<h'q:�D��8j"�S�5��!=Ҡ���P�YВ�t����u��P3�D��=2j��
�#�5Qsm�L��͐��!5Wcj.�\JCΥ4�\JG�
�*t��Б�BG�
�*t��Б�BG�
�*t��Б�BG�
�*t��Б��@������R_��H}=��z ��@������R_��H}=��z�uY��z���'j�=|���'j�=|�f�Á�z������B��5����DM�f�>
R}��(H�Q�� �GA���T�>
R}��(H�Q�ꃘ�M�Hn�L�
�L�D��fĮ���Fr3bW�D��fB�f�t����3,HgX�ΰ �aA:Ât��R�R�R�R�R�R�R�R��R���"�kE�׊̯�_RsRsRsRsRsRsRsRsRsRsRsRsRsSs!g�93l���C� ��uD������-�jY�������ʷk��y���Wӽ�v��6_�[����U|�F;8�a�	p�s:��Gt&�!O�L�C̙ �<�3�J2qI�1H2�1�[$`ӊ1C2�1�$`ӊ9,�	0�i����ƴbFgLcZBcZBcZBcZBcZBcZBcZ
cZ���#jZ�TБ	0�n�rd�[Ae� ��VPG"��Qh��"D�E���q̿p'S;��PWY�V�0�i�"D�E�����.������^�y�0��;�sDM��u�83ے�nMH�xm���������VP�[&�0��ܖ	0�n�|����<�ygȏ�i�:��+�̄�^ �uPKW&�0B�͕	0�P5r%L#�A=\� Ә-(*���ƴhAqP�V&�4�EK��v��hgM�v֔�|yDM�j���:ͫu�W�4��i^�oݫ=MO��j����T�	�Y�N�j�f�:ͪu�U�4��iV�ӬZ�Y���G�4ף�\�Js=��]��D\����Z�/]��=�+���[w=.�=*���4ۣ�l�J�=*���4ۣ�l�J�=*���M�UڄZ�M�1����;y�N�v�$*�*}_�/�^6��׶n�]F/��k4#�ь��u#������7?�f�5���hF^�y�f�5���N����5��3Mw�(�4Q�i���Dq���N�}��b�4Q�i��o]�Y�e-���{;;e�i���Dq�=:�F4�8hq�4�i�Aӈ��M#�F4�8hq�4�غF�0�i�4",8��Gg�eٺ(>M1͏���쇜���.��>~;ꝍ��7�?zV>=�����#+�ۣO�٧g�����̺yO���7L&O�,�<�d�̒��I�q�u9��s��ӘL&�?0�r�J'�C�[�P�ܼҡFn0����C���i>I�y2&�%�"�t���Lb�|�S7�6�(iQں�}��^�;xQ���ƽ���߯��|�_�y�(�@���e�
i$��,`TH#Qg)f�4�xfL#Z��Fb�t:�$}�4�$[WL��I&�I&�I&�I&�I&�I&�I&�I&�I&�I&�\�0�i�� Әk�0�i�� Ә�И�И�И�И���q�~���-J�[���(-oQZޢ��Eiy�����-J�[������[�N�@���������x-m!9��i��h��h��h��h��h��h��h��h��h�ɶ.�.Ll�	�O���f45�5��&(H͇� +��D�_qr9`�����u�(��Di'J�8QZǉ�:N��q�����q��ƴX�4��9`�b�Ө��<���[J��RZ��Ҫ��V����-�Uo)�zKi�[J��RZ��Ҫ��V���ꭠ3��$����N�+����N�+����N�+����N�+����N�+���n��������'j���'W�qr�'W�qr�'W�qr�'W�q� �#�r�r�r�r�r�r�r�r�rݢ�N3l�f�    :m�ŷ~Zm̿�Xhk�ɪe_�y�4����j��_���MA�/g?`bU�0@:�!3jdU�L�ٿC:ӡF����t��IEM���b
ҡFr��*�t���,fr:�Hn�9���|�YS$F�<6Z��:���yl��c�u���h��F�<6Z��:��N��SBS��E�u-�k�h]�F�Z4ZעѺ�ֵh��Š����%���h�t��b��[t��������?|�a��Kg�^:������h�t���N�����&��<����X��������/�o�߽���oo_���߾�IX���B�^>�����^��z���h�^F��2Z�W�-+���z���h�^F��2Z���j��V�e�Z/��z���h�^��֫|�,ˮ�Yv_m�������b[���,�&;��i����Cݺ~8���7z������?|�a���C�ɇ�u�p�	�~q>�E���y�0�Pi�Xw3Q��V����u�u9`�j4��ht�m�n]F��n��ڐ/t:~I,4d�!K���]ҡ&VEͽ�,�C%ˮ,�gW�u9FM��0j�}�L:���1h�gQ�_���]�r�˲/�~6����dC���A2�P#�rIаL:��ox��L:�D�4A�5Q~�ҤCMTAS5�P#�GE���T��>�[+rbEnA��-���"� V�Ċ܂4���؇٪��ռX��}ߗ��F��}�DJ4��5�����CM��A��P)i�hx*���6j�	�`�	�`�	�`�	�`�	�`�	�`�	�`�	�`�	�#`�	�`�J�w/�iL+&��xN��h��r(I��	4-?���Q+�O�aw.Ԋ�`�FD�h>�iDԊf���~�}ȉ}|Ef���e^����r����{����{9`�@��=���
�`��YԹW��`ӢY�J���fQk���L�iL+�TT&�4�s*��cP� Ø�\�5ͺ4�ui4��h�e�&�L�a�:h�T&�0B�	!`�u�� `ӢY�F�.�f]ͺ4�ui4��h֥ѬK�Y�BcZBcZcZ���#j��(.`؝+h=\&�0��.`�F��	0L#����yD�u~y\oY�a���Ut?�:�� ��ˠ%`?��~������l���?�y՗�Խ�^����	����i%�r�g�?����-���޽}��ŧg��}����r�B��ͧw�����	0�q���x���r���W�%�-]W=@����k973�ϗW3D�u	u��L�PA�Q2�i��u(� o\C]&����dLA[O2�1�Ԃ�^ Әm`���m`���m`���m`������O�u?,O(�Z��zo�+�U�iVm�Y��f�v�U�iVm�Y��f�v�U�iVm�Y��f�v�U�iVm�Y��f���V���49����}-c�M0�iѬ�N�j�֭��/|�K��u��<�S۷��^�F�h�&!��tf��yӃ�M�7=h���yӃ�M�7=h���yӃ�M�7=h���yӃ�M�����8桔M�6�6��M0��4��M0��4ӞM0`f� 0�(���iL�L�4�s&`�¹ �	�	�WaZ��ܒ��K1�t6Oȓ8j���ҡf~�!�+���5q5� F��U�u	��Kd�(H.Lc��C��f1Y:�Hnc��C��f1�:��V���HI��"$E:H�t�� )�AR���HI��"$E:H�t�� )�AR���HI�RL;X:�HnS�5���T��C��f��f��f��f��fJ�f��4�i2�4d�i�LӐ��!3MCf���4�i2�t$#u$#u$#u$#u$#u$#u$#u$#u$#u$#u&#EN�9r�ΑSv���s䔝#��9e��);GN�9r�ΑSv���s�YE���W�^�~xE���W�^�~xE���W�^�~xC���������7$oHސ<�!yxC���������#�R:R}t���H�ё�#�GG���T�>:R}t���H�ё�#��@���T�>R}��H�1��c ��@��ATe!r����&j 7����l�r����&j 7����l�r��0�����܌�O�HnF�K���܌ؗ2Q#��/�,ľ�R��YA�f��oV��YA�f��oV��YA�f��oV��YA�f��oV��YA�f��oF����܌�3<Q#��]y�&r3Af��>�}2�d�!��C�ه �Af��>�}2�d�!��C�ه"��"��"��"��"��"��"��"��"��"��"��"��"��"��!5�!5�!5�!5�!5�!5�!5�!5�!5�!5�!5�!5�!5�152�3��&C�lr���HW����#]G�
�t�*8�Up���HW����#]G�
�t*RsU��H�U���"5WEj���\��*RsU��H�U���"5Wej.d�[�InE&��QY���Qِ�YC�f�5�o֐�YC�f�5�o֐�YG���d)�R:��t$K�H�ґ,�#YJg�d�ב�^G�{����$H>�<| y�@���������$L����)�����);Y��z���'j�=|���'j�=|�f�Á�z����������Gr3����;������*H�U��� 5WAj���\��
Rs��*H�U��� 5WAj���\�Ls�Fr3bs��LA�&>��*�U�� HWA��� ]A�
�t�����������������͔�͐��"E&>�L|��(2�1��6��6��6��6��6��6��6��6��6��6��6��6��6��6��6��FNTq�8�Kq���H/ő^�#�Gz)��R�8�Kq��R�ܬ"�YEr���f��*��U$7�HnV�ܬ2�2�Ȝ�s.]���G�1�O��n��R��e=���W���߯|��;��ڪ}5�[o��l�e�e\M��X��m��C~ԙ ���3�Y� �(�L�C�U&�!�*�I�	p��J8FHeLcZ1�)`ӊ��ƴbF3�1��A�L�aL�gbQӌ����L�at+�#4`�
j�F���@3��-��%4cKhƖl���/�-� �l]|_�? �1-��%4cKhƖЌ-Iml����	دD�2!��՟ ̜S�[���Z�x�t�r�0�ԭ�	0�n5�g�[��|G�[g'+�_�wJ����<`����>��g3������L�i���f�q�u9`��z�v9`Ӣ�jA�� Ә-V�Ա�} ���rQoݺ��c�Y�A�ϙ �uP�s&�0BT�	0�P�:'L�.�f]:ͺt�u�4��i֥ӬK�Y�N�.�f]:ͺt�u)4�%4�%4�%4�%0��[rDM�[�v9e{6mp��l
�۔	0��֔	0��є	0�i�򖠥L� Ә-o������J�[*-o�����������5���hN^�9y���5���hN^�9y���5���hN^�9y���5���hN^�9y���5���hN^�9y���F��n���F��n���F��nBcZBcZBcZJcZJcZ
cZ!��t�x��❖�wZ*�i�x��❖�wZ*�i�x��❖�wZ*�i�x��❖�wZ*�i�x��❖�wZ*�i�x��❖�wZ*�i�x��❖�wZ*�i�x���]iL���w\*�4���
Ӫ�21���Y�!��ls1�C:�!w�t�Cn��P�ܷӡ�y'C-13\�P�3͕5�w���e^ٕ�+�U�غ��:5�i^��\�����������R$W$W$W$�m��5��+��+��+��+��+��+��#�Efs"#j$7�9��5��Ŝ�Ȇ:�F:�Hnsh#j"73�3lHgؐΰ!�aC:Æt���6�3lHgؐΰ!�aC:Æt���6�3lHgؐΰ!�aC:Æt������C��f1?�P;2�pd����Ñه#�Gf��>�}82�pd����Ñه#���T�>*R}T���H�Q��"�GE���T�>*R}4$#mHFڐ��!iC2҆d��H��6$#mHFژ��7�ސg�,@C�hȳ y�!�4�Y��<А    g�,@G:��w�3ܑ�pG:��w�3ܑ�pG:��w�3ܑ�pG:��w�3ܑ�pG:��w�3ܑ�pG:��w�3�l��Ȗ��l���c ����>2���c ����>2���c ����>2���c ��A�>�BT�5�n6Q�f5�n6Q3�f@�9Q��DT�5Pi���4'j$7#*͉�͈Js�Fr3��D��f�)���͈�f5����&j$K!ΛM�H�"<�bKfG�1�O��n��R��e=���W���߯|��;��ڪ}5�[o��l�e�e\M��X��m��C֙ �<�3yDg�t�8���	p�39�� �� ��'`ӊ	o2�1���&`��wDMӈBӈBӈBӈBӈBӈ�u�8�_�[�UY��T�w����������w���;����������뛻�L6
M6
M6
M6
M6
M6
N6��eL#_1�}� Ø�&���+��bW�΋���	����y?J�~���(��Q���4�GS{?�/wG�ƪm��U0�~l��9}��j��e/���|��<J�y���(��Q�ϣ4�Gi>��|����� �lF��ƴbΧfLcZ1�R3�1����� Ø�\�5-_2Z�d���hq���K��`�`[�[.L�[���-o1Z�b���hy�����-F�[���-o1Z�b���hy�����-F�[���i	�i�:���i���5���rZ���i��"D�E�N��!:-BtZ���i��"D�E�N��!:-BtZ��������~,o˚EmtI�:�є5o�E-xI��G0�H�z���:j�I�� j$7�qҡFr�� �!jA�xZf�5����J���͂\�CM�fA{�ҡ&r��W�P#��7Zx�5��!}���W�P#��7�o&L�,f�&j$7�9ʟuЖ�t����}w�P�G��t���#h^:�D���5�ym��uPkz:���uPz:���uP�z:�D�0�S=j$7C&�A��P#�2�j\O��͐InP{6��$7��=j$7C&�A���P��A=��P�GP#n:�D�ԍ�5Q}��fC�t���rӡFr3�3�Hg8�C7j$7C:�A���P#��j�M��͘�p���t���,f{g:�Hn��3�̹*2�Ȝ�"s��̹*2�Ȝ�"s��̹*2�Ȝ�"s��̹*2�Ȝ�"s��̹*2�Ȝ�"s��̹*2�jH��!=҆�H�#mH��!=҆�H�#mH��!=҆�H�#mH��!=҆�H�#mH��!=҆�H�#mH��1=R�Y��<Аg�,@C�hȳ M��L��L��L��L��L��L��L��L��L��L�ܬ#'�:r�#'�:r�#'�:r�#'�:r�#'�:r�#'�:r�#'�:r�#'�:r�#'�:r�#'�:r�#'�:r�#'�:r�#'�:r�#'�:r�#'��"�r�#'�:r�#'�:r�#'�:r�'�|)��#��'�u��n)W˲����OW��W�]��kmվ��W����2�&�e���6�y�!%�vr���w�o�R�z?=8��d��C8I&�!t$�&�	p	�8�J8f�*`ӊ��x�L�r�0��s7��iQhQ��/L�s�4��4��4��4��4��4"/G>���K��K��K��K��K��K��K��K��K�Y�J��f�+͒ט�̙ ӈG���L�i�#��r"�1�3�1��Sʙ ӘV���L�aL�w�鈚��͍7�o47�hn���x���Fs�y猏�i��i��i��i��i��i��i��q�6�YS�?�*l��i���u��J#ԕF�+�PW��4B]i��n�P�ݢ�k]���Dmo���̓���o�÷�_�W�?޼y�/�ş/��}`܋חxDM�؍F��b7�n4��h��(v�Q�F��m��R�!_�t����e���N㗝�/;�_v��4~�i�2h�}"�8~I3l������wDMS�� MA��41h
b�Ġ)�S1�!݊�P#�c��˥C��ƈi�K�Z��Q��Uh�Qe��Q/���B[�*�ŨB[�*�ŨB[�*�ŨB[�*�ŨB[��>2�d
���>f�d��	ڞP��	ڞP��	ڞP��	ڞ�`ӢI&�I&��`�b-�: �1-��`�b-�
�;�9y���B��.���B��.���B��.����md��2�.�^���K�y��AqZ:>�'��k�P�<�ӡ&�	�m�P#�� �eo�P#��P_L�5��ńr�P#�YL<�5��	��	��	��	��	��M�CM����CM��M!�Cͼ��u�PB:�D}4��5��!�Ef��>�}(2�Pd����C�ه"�Ef��>�}(2�Pd����C��L��L��L��L��L��̐��!�kC�׆̯�_2�6d~m��ڐ��!�kC�׆̯�_2�6d~m��ڐ��!�kC�׆̯�_2�6d~m��ڐ��!�kC�׆̯�_�"�2�6d~���ڑ��#�kG�׎̯�_;2�vd~���ڑ��#�kG�׎̯�_;2�vd~���ڑ��#�kG�׎̯�_;2�vd~���ڑ����`�sDMZ�{Z�{Z�{Z�{Z�{Z�{�d��{L��{��{LcZ1�M&�4��������c�q���&��&��&��&��&��&��&��&��&��&��&��&��&�'�cF���N������e>��F�b�� �E���]�/��o����EM�y���󍫴+9���ފ�#A[�3����� ��G�F�L�a6O��L�iL�f�-��ƴh6OЦ�L�iL��y�U�:Z!��}og�rQo����{��*`��8h/U&���q�6�L���0�0��Z?�	0��Z:�	0�i�l��-S� Ә��Z+�	0�iŔqeLcZ1\� ӘVL��}O����Z6�iX+hgT�O�ƴb��2�1���P� +�i�z��i��A��2��ˠ�H� ��e��L�a�2h�Q&�0~��(`Ӣe�A;�2�1-^��5M#V�F�4�Xi��4b�i�Jӈ��+M#V�F�4�Xi��4b�iD�\m���V�\m���V�\m���V�\m���2w_6�W�h^m�y����6�W�h^m�y����6�W�h^m�y����6�W�h^m�y����6�W�h^m�y���Ն|��9y���u���iN^�9y���u���iN^�9y���u���iN^�9y���u���iN^�9y���u���iN^�9y���N���6Ӿ�6��o�i�vm/W��e���K#Z��L�_���������`�
�Y��A�-P�@m��A�-P�@m��A�-P�@m��A�-P�@m��A�-P�@-�ge3��3�&`ݚ�Ytkfѭ	�E�&`ݚ�Ytkfѭ	�F�`��LcZ0ck�1-��5Ә�ؚ�iLflM�4����iL6)>ӘlR|�1-بx�N��Z�RhyK��-���Z���6`�T��	0�p��ƴhyK��-���Z�RhyK��-����_&o�k�U�~z0-o)��������v�u�L�:�g&C-1YD:�Ļ�Ĥ�P�f�O�C"�ӡ&��^A��	�d� �c�4�4�4��4cb�t���,&�H���b��t���,&�H���b���CM�f�L�(2Pd
��Ht^��uak������0l���m� �,��e���b�Rץ������5�(2Pd��@�9�"s E� ���(2Pd��@�9�!�BC������6��5���6��5���-4�[hH�Аn�!�BC���t��-4�[hH�Аn�1�B�԰!��95lȩa���O��������͔����:2�ud���$בI�#�\G&��Lr��:2�ud���$בI�#�\G&��Lr+R_W���H}]���"�uE������+R_W���H}]���"�uE���ו�����9)]���9)]���9)]���9)]���9)]���9)ݐ9WC�\�s5d�Ր9WC�\�s5d�Ր9WC�\�s5d�Ր9WC�\�s5d�Ր9WC�    \�s5d�Ր9WG���v�[ؑnaG���v�[ؑnaG���v�[ؑnaG���v�[ؑnaG���v�[ؑnag��ȩ�����ȩ�����ȩ�����ȩ�����f���Dn6���@���_d~=���@���_d~=���@���_d~=���@���_d~=���@���_d~=���@���_d~=��uY�)�D��M����D��M�̻PiN�@�9Q��DT�e!� 5��S���͈)�D��f�`�Fr3b
0Q#�1���܌�L�HnFL&j$7#�b���܌x�m�Fr3%r���42�,�L� 3͂�42�,�L� 3͂�42�,�L� 3͂�42�,�L� 3͂�42�,�L� 3͂�42�,�L� 3Ͷ��#��'�u��n)W˲����OW��W�]��kmվ��W����2�&�e���6�y�!��L�C�b� ���2ѕ�x̿p'~U�����1�<�I��Q�� ��L�C4d"�1�~&�4�c�g��i��?i矲/�S�iL+����ƴb|�L�iL+ƭ�o�$W����E���c����y^�5����'4'OhN�l�ɻ0�_
���E�GԴ��n�a<Yu?~�˪�Z�/v'���y�ȟ/��!߼�~��<{������������[>�T��⫕�|���g����w������<{a��<-�_�y}�-�=�u�Ol)���J���Yu]�c�}��߼~���۷/_���޽����<}����r�-��3����V��|ު�4Niis��h,Ͷ��.�d�uJv9`��� �q�u9`�2ݢ?m��h�O�z��^ Әm��h�OF~��?��3Sd]�wN���<���C�y����g����ˏ�����#8���>N�}�f�8��q���4��i���l��>N�}�f�8��q���4��i���l^?�5��4��\�Js*��4��\�Js*��4Q\i���Dq��b^����%���K�*}U_��z�c>�yx���ի�7o��>{��ͣ//^�G��l�fv�=h�3oZ��i4�h*��TT����uu�i�����u��"���DT���F�RM56�j�e����9-��8j��Z�U��5�G�ְE5#�C�t>�k[TQr6�����Hn\kآ�����"�V�E���׶�w�U\
{���e���v�	mם�v���w�]�U	mם�v�	mם�v�I�]w���5�}�w�~��o+v�yeY�|7�y�4��p�4�N')�D3M0�V
	m���V
	m���V
 �������L��ƴp��u&� �ƴXE)�4���~��Әk9x�ߑ�Ƀ�#���#�0�_n~���a�r�F 0�_��
m��Ls���)��S���4'OiN���DM3{�f�X̱�L�ac����06�=�f{��l��aBcZJcZJcZJcZJcZJcZJcZJcZJcZJcZJcZFcZFcZFcZFcZFcZFcZ��0����?r�3[3�]�KԈ��A�/ӡ!�?��P�!e�p�%rm=�|˾�D4F����N��xT h�:j��:h�:j��:h�:jb��!Y�!Y�!Y�!Y�!Y�!Y�!Y�!Y�1Y
r	�!��r	�!��r	�!��r	�!��r	Z��d�� 9�Ar���Hɑ�#$G:H�t�� 9�Ar���Hɑ�#���\��*RsU��H�U���"5WEj���\��*RsU��H�U����Wdj_��}E����טz�t���,��7j$7���M��!������oH?�!������oH?�!������oH?�!������oH?�!���t:�U�HW�#]��t:�U�HW�#]��t:�U�HW�#]��t:�U�HW�#]��t�UHWa ]��t�UHWa ]��t�UHWa ]��t�U(Q}L��o�D�����DT5P}L�@�1Q��DTe!����͈�$/H^�<� yxA���������$/H^�<�0y82(���͈��5���&j$7#6�M�HnFl𛨑܌��7Q#�� �� ���Y_�9�u��[l���eY�n�է+��+߮�N浶j_M����+�|�nW�2V�}�<���	p;�8��d�I��ᮔ`���^��a#�>�"�	p�8�J8&���F�br�L�iD+&��x�D�r�4�����돨i���\��Bs=d���ac��B�=�f{����!4�Ch���l��B�=�f{����i���l��J�=�f{(��P���4�Ci���l��J�=�f{(��P���4�Cq�G̹�L�iL+�u&�0��;uۣN��C�;uۣ��C�dN��׈ңξ�C�<al��QGaҡFr3`[]�:�5��	�Dh� B���� �p��Ѯ�v_���}�݋uU�9a���A.��0�V"�j�U��Dh� B�Z5H�i�t�)H(&��i�	V+ �Z��
�V@h�B�Z���j�V+ �Z��
�V �I�ӈ�� N�5qD �]�CM����P#�7�w:�H'9�D�ӡFr3��fP�S>Ԭ\Ni���|��^ ��"��2*m�Q�?�x�F;\5��þ�����6ʨ�QF��2*m�Qi��J�r��-gJ�rT��� z�J��VZ��:�������o�u~+��[i��J��VZ��:�������o�~��� d���}/�<^ѢY��o�U~i�t6��l��<F�y��y�ԫ�������g��f���1��c4��h6��l��<F�y�f���m��Ә��1��C<�5Q�l��N�=��=.{;��p���4��i���l��N�=�f{8���_���iY�^��D�Y�A,`�'N��X�4Y�5��Y�4h�5rvY�4x�5��Ō �C��f��fB�fA3��Pi��y:�DF4��5��M��CMd�A���P#� )�t������C��fH�0h�=j"7dO����iO����nO����sO����&z)AS��P#��K	�O��͐^�#�Gz)�*T��P��BE�
�*T��P��BE�
�*T��P��BE�
�*T��P��BE�
�*4��А�BC�
�*4��А�BC�
�*4��А�BC�
�*4��А�BGj���\��:Rsu���H�Ց��#5WGj���\��:Rsu���H�5��k 5�@j���\��Rs��H�5��k 5�@j���\��Ss!�gpzv�k��>�&-�>�R<�R<�R<�UV)�׀AK�O�AK�O�CL�L�AK���IK�O�iL����ƴx�5M2	M2	M2	M2	M2	M2	M2	M2	M2	M2	M2	M2	N2�$�� ӘVL~�0o bDeL�P�"FTڔ5oXuD�N�P�UGT�5r�	8�:��t���8�z@M�fA	d>Ԭ�UhêBVڰ�ІU�6�*�aU��
mXUhêBVڰ�ІU%��ƴ��$G�õ1_�+��m��Uo_y�*�J�u��W����i�6�,�qd��#mYh��BG�8��Ƒ�6�,�qd��8�]�=����r��̓��_��#[]����^]y��������4��ɬ	�`�*4�Uh�Kh�K`�8�7Q�v3��c���Js3��f*��T���47Sin���L���Js3��f����K��L�4�d4�d4�d4�d4�d4�d4�d4�d4�d4�d4�d4�d8�D��0ڴ�Ѧ=�6�a�̴G]�Vk{-g�N�&�ӡ&���ΧCMl���O��`�l��O��F![肆ӡ&�͂�ӡ&�͂ӡ&*͠��t��J3h�1j��sL��͐JS�JS�J�w�1�^:�Hns�;����P#�� �� �� �� ���Y��t����sH�P��A'�ҡf�É�:�R:�D}tT)j$7CfA'�ҡ&r3Gj.Gj.Gj.Gj.Gj.Gj.Gj.Gj.Gj.Gj.Gj.Gj.Gj.gj.伙#��9o��y3GΛ9r�̑�f�V�[X�naE���V�[X�naE���V�[X�naE���V�[X�naE����겔m�
��au�n�;)W���Zً��Z��0��+ %	  `N	�W�����9%�_g.��õ1_r�I�������+�W�+�۱"������
8�*���m��m�+`�m��
F�p��	5M1	M1	M1�������d�LA[v2�I���:?�|���W����E����U�oG�׮���o�����;�y���Ϗ��?�����7_>�}h䋦��������O�i**h�L&��2ME-�������dSQA�cN���0�i�$n$���� �Z�3�)��n�L�a
"��9`���a�� �ڗ3�1-����.AM˙ ӘV��L�aLw&��w&���y&5DI�C��9����Qo�����yp��rD�gx}!G�Hn���Q��l���G�
؅V#�j�U��F�W��`��+�n�u����a�n�]��b���۝c������������OO�7�/7��k���|�2w���H��	0�|}m�	�Hh�@B+Z!��
��V$�B �I�B�{LcZ4Qk�9 �1-�(f� ӘV�0H&�4��`:`⬦�J�VJ'�R:���	��Nh�tB+�Z)��J�VJ'�R:��� Ә��S���4'�xvQhŒB+�Z��Њ%�V,)�bI�K
�XRhŒB+�Z��Њ%�V,)�bI�K
�XRhŒ�4�E��1�<��+�K:��>���.�P#���]�Ag_ҡF�)Gv��I���͂&��fe˚�S�|���We9�5��U�;�߮��>n�^�<x���?�~���ի������[[����7w�P�U,*�bQ�W,���t]�؋��������}��~Տ�����|zQ>�<���˻?߿���涝Mi��J+]TZ�n�t�r�4*�
���hX�������Wk�=������_=�~��淧�����'�ܾD�Di�|J��SZ%��*��Vɧ�J>M^�wGH��|�'p��Q�o��/܉���|O�n>?����^�.�_�ҏO޼����}|z�=��*XI�n����܏����7ν�Y�W�k��i܋5ԫ�����t� ��Ki_J+�RZ���
��V𥴂/�|)��Ki_J+�RZ���
��V𥴂/�| Ә�h�0�iϐ(��Ni�uJ�SZg��:��Y���:�u�)��Ni�uJ�SZg��:��Y���:�u�E��\ �� Ns��8�p��4�i.��\ �� Ns��8�p��4�i.��\ �x��z����f{T��Qi�G���f{T��Qi�G���f{T��Q�n{��U]��VY�Uo�^�<�cy�������ǯ?���Z��?��}��d&j��h4M�h���4E�i�F���)MS4��h4M�h��m]S\�ƴhQj�C)�F�4��i��4b�i�Nӈ��;M#v�F�4��i��4b�i�Nӈ�i㶝6n�N��0h.��� ���0h.��� ���0~	��h��?�k��iL���_����vh��1?�l�)�W�5r{��9jd	y���uP�z:�ĻYP{:�ĻYP{:����Z�t��S�A��P#�fPU{:�Hn�T�A���P��A���P�GP	t:�D�T�5Q}CgC�TA��P#�R}���CM��5��CMTA]��P�GP+k:�D��Ϛ5R}5��C��fL������)�s�6j$7+HnV��,�e;j"K	��N���R���ӡ&����t��,%��;j���˝5��!����t���,��;j��,��;j��,��;j��j�N���4�:���F*͠v�t����4�ʾӡFr3�BG�*t�BG�*t!r��v�t���,��>j"7j�O���͂��ӡ&r���l���YP~:�Hn����7H�l}����D��O��{�D��O��{�Dͼ���D��5P_����'j$7#���͈�z�Fr3��&ΥL�DnV�^JAz)���R�^JAz)���R�^JAz)���R�^JAz)���R�^Jaz)�3>E�� $A:H�t�� 	�A��$HI�� $A:H�t�� 	�A��$HI��"]E�
�t�*(�UP���HWA���"]E�
�t�*(�UP���HWA���2]�\�"�R��=[��73�ofH�̐��!}3C�f�����73�ofH�̐��!}3C�f�����73�ofH�̑��#}3G�f����9�7s�o�H�̑��#}3G�f����9�7�H�U���"5WEj���\��*RsU��H�U���"5WEj���\�����
9�А��!�uC������R_7��nH}ݐ��!�uC���׍��}q�Q�h3;�F�����������e      �   w   x���;1E�:Y��h��cǟ- !�D7�_��1J��y���lǽ	���w�^CK�܇꼰��o&���om���8�fYPc䚗�^���g$�<��m@}5�t����{Tq>f      �   �   x�u�;�0��99EwT㷝�!uc��G�0�����M�q<�d��KbU���iTYv�u��]2��nh]pt��!���Qy�d��wm DA|���L�6���wR0����Յ��/����+ARS'����j���WF      �   �  x�UV�r*9]���%�м�%�r/�P@M6�ݦ��mSn;)��G��c�RI|t$Kǒ��n��X%.�?��[ݧy�������T�[=�����t2%�7 ���Qy݇-��*u)lRK��K�[(�3G�yi
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
�ؘ�С*k��C��I%����|����?��0��3_���~Wl�)�3L�GL0T�t"�0=�7۰`zC�j���5S��)F`Z1���~�0�vwf���a�-c5��Τ�355�nŗ�:��ē���R���U���*q'Hޙ �Ö�<|�#5�x܊��/a�&kv�dז )SHwɻ"�69�J�욢��)?n��/(�̗      �   S	  x�͜M����=�b�(V���%�7@�d�\������0����[l���DJ�An�ck��zTo���w�������w��w���`����|!���=��篟��O����&���h�0}3�s #%?�E�C�>����_�9}����?�}���^���v�1L����'ra��/?~��L~�4��̫	�,���9�j�������_�g���+������X����� Y�� �k~z�Y�n��?���ڗo[,p�"Qj,#L�'�������8�!�1$t��z�����aa����C�;W�3�`�*��E@L��
�O.��%8o8�b�/���k�ZB�0xFm��ƀv��đ�5�,#��g{pJ�!����=�9@��G ?'���8�e�2��W��s��byt��|�؊��rh��b�Qp]��+��p��`
n���q BN��I#��K�o]X>�K
~6��
�A �v�ϖ�%L��|�dK�+!�P^�_{qA(����
U�  ��Y��(>(�Ɔe��o�SfQ�
�$�� ���$�Ɉ-"���a�J�\�����c� ���z:P�ǐ#���|�X4$���k[�B+N��A�Y8X�ɱ�I��1[��K�p�(�+ωBֲ��(A5a V �|N \�Ӧ=% ��¹��M��\@��>
���C!�%�Ip6��@'u�� �%�v0Ȇ��鸊N-�]�	��p���4��V��zf�d%TqH-+y���H�VGN㼲�lfF\Ej��W��UR��!��)p� n!�3�� ����g�A�T�7|%�
�o��L�Ԓ��w���Է��қ�qKI�O�L�7��P�I�]�L���}��W���P{ԉ�h��E���m�h�ַ�yA>��@Bɲ>'E�.�T!�+~�c)-�jL�.C��7+���z]ֲ�S��x�z�UJ4Rh7o6�8D�8I� )c3 Y�e�DZ�7� ꒨�h�fr�h߸�� D	,���۵�[���6����`]�E�n�a��	B~������hG,�g�Zzw��s7+:e���C��f�#�}�~�65b��I!D\.Q�(����'n�G#d!=T����h�nϚ	X�?��Ԩ���
�p�j4J4���'ӉqD�Ӌ���C)(�P�@�f_�ȍx��"<��r�W�� uU��5����` ���\�i�#��∀�dX���RE"��<]����{�	�Rͫ� [���O�@T��A�jԵڪOI���֠%�:�~?��e�|��uĀsIʎAXcH�r�1AYyR�<]7��/D�4���E>���eU���5z��:�@?Ф���%�r7]��l	�2����j֜ ;�u�+héD'��Q�-���5��B,��,��{�cu&���kWrǶu���Tj��5	Q��uɪ�9^�`��6�JrR
>Ł>.�؉���H�|"%�O��RIO��y��z�u��K$p?��8r�Ń��v��P��D@�Q�"�R�U���-�SVt@~��a�T�4^���0�J�����B�F������(3�!4t�x�b�R�L���"�����pR���>�4M�z�+��c�)���@h���s�ok�@�e�j�H�}%���,�l�1�j�~��7(t���ae��[��h�w�1�xڧO=a���i���@CR���H�Z�7�K�\�O��n�.n�s7A��$ڱ2��RMZ ������`y`&v�?gSP˼JxP�k9� �u�� ��{�/V�*S�5���Oy�B�r(�CFȺR�!�p �����{�Nt ]��"!��O�Lv�%
\��*��Es���(l�J�T!���cE"5`�X�/b*:��u����X&c�$S���[E�(�5^�@��g�F�<qH�%�����Vuqt<����l��-��a�۬n��嶻|��������M�z�=FK�`󝭗$nw��@D��F�+���=�;�lb���A�l���u��&�y���A:�*� NO[�8�3^�"6�����a8�sɵ6���%�2�MX�f����7m6�fӚEy���3�x��h��g&�:�`+0�[F�`�i��;B���<!��_!���z��,@�"AccM�ţf�p��Dw*8���c���U"�I,��.��E�@�&*�#W�h�]f����W$D�`(�+�	�EY�Cl��=�a]�>��;���'����G7:�Sj�}��o�кsӊc���j�	|J#*��Ϸ\���s� ��ɔ���dj�/| �Jd�]0�fI�wW��.�5�����w���������� �d     