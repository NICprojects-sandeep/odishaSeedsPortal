PGDMP         (            	    {            stock    15.3    15.2     z           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            {           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            |           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            }           1262    41917    stock    DATABASE     x   CREATE DATABASE stock WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_India.1252';
    DROP DATABASE stock;
                postgres    false            �            1259    42007    mCrop    TABLE     �   CREATE TABLE public."mCrop" (
    "Crop_Code" character varying(4) NOT NULL,
    "Category_Code" character varying(4),
    "Crop_Name" character varying(100),
    "IS_ACTIVE" bigint DEFAULT 1
);
    DROP TABLE public."mCrop";
       public         heap    postgres    false            w          0    42007    mCrop 
   TABLE DATA           Y   COPY public."mCrop" ("Crop_Code", "Category_Code", "Crop_Name", "IS_ACTIVE") FROM stdin;
    public          postgres    false    233   �       �           2606    42090    mCrop mCrop_pkey 
   CONSTRAINT     [   ALTER TABLE ONLY public."mCrop"
    ADD CONSTRAINT "mCrop_pkey" PRIMARY KEY ("Crop_Code");
 >   ALTER TABLE ONLY public."mCrop" DROP CONSTRAINT "mCrop_pkey";
       public            postgres    false    233            w   �  x�UVMs�8=�_�#�,�`��L�����\����V�%J��b�v�E`�R�������k�n�����P��V�i��	{�$��RV�<3<j!�E�R:Q+����*�32o@�+E{�30�뵺��\C~��Z��ʥ3�~�IS�N�%��Y�1h�Y�G�[F�\:)M"n�)�Q��l�5e�M����+M^4�����JhD�EI]����X�+�y��0�����/����C�
�VL��)���{)�F �ڄ���ˈ�V�A�h�tsS�G���iym�����: ���W9h�ؘ4]��~�:���vpB���^�o�jP�U�g�XSbaDN����w̓�.4���u�����j��~����s���ua�PN�^ۣ�ά�D�������A�~�����1�����}�3!NR��2���dN���m���k+Uy}x�+o�Ji����0I�pg�*p����G��'M^=�cz��(���G��4�:��r)�)���M�%ʆA�;��#)���(�{�n�3&�XMT�V.	3�c���_t�*4������02���W��{T9d���w��?��v<���:��'G�v�-�h1#��เS���Fb�`CJ���.Y�QQ��"�[��f�x�h*���{�G<N'F��8J�x���9*�h��v%�?i�N{ˠ����������_�G�
���UbQJ�ȟ���)=m?e���$r��$�kp����H��H?����E-l��a0��Bc֥�&xJ}J�W|Ǩ�Te�\�7�z��a�0��|��������>��!��t���O�ȶ�y��(]�&��SK��?�8�J���E�v*��w�I;���X�
n׾�U��3ʶ�MMF�f��2�Wp�j�:��/�Ƀ|8<�q����ɰ�����aښ��a�W	�㈕���qu�G�4x�qD��ЌL���e-�x�2�1e�9��<�2������^DC����"M(q=�����m'1\rZ��@V�jq��7�a���X��p��k�S�LbΎ�$�Ǩ�9��M�U̢�[���k�E﷡���ˢ�;(T�Rg��*�M�������~l��dt��Q��ﴴ�d�sn�������4
���4�0ǚ�ҙF��J��i���2mIF�ܫk:�o^���.�1}Q�|��鄠5N�+.e���'�������u�suG6���xI+H�k�@+����_OOO�Y���     