PGDMP                      	    {            stock    15.3    15.2     n           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            o           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            p           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            q           1262    41917    stock    DATABASE     x   CREATE DATABASE stock WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_India.1252';
    DROP DATABASE stock;
                postgres    false                       1259    58354    mLATESTNEWS    TABLE     �   CREATE TABLE public."mLATESTNEWS" (
    "NEWS_ID" integer DEFAULT nextval('public.mlatestnews_sq'::regclass) NOT NULL,
    "NEWS" character varying(5000),
    "UPDATED_ON]" timestamp with time zone,
    "IS_ACTIVE" bigint
);
 !   DROP TABLE public."mLATESTNEWS";
       public         heap    postgres    false            k          0    58354    mLATESTNEWS 
   TABLE DATA           V   COPY public."mLATESTNEWS" ("NEWS_ID", "NEWS", "UPDATED_ON]", "IS_ACTIVE") FROM stdin;
    public          postgres    false    257   �       �           2606    58361    mLATESTNEWS mLATESTNEWS_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public."mLATESTNEWS"
    ADD CONSTRAINT "mLATESTNEWS_pkey" PRIMARY KEY ("NEWS_ID");
 J   ALTER TABLE ONLY public."mLATESTNEWS" DROP CONSTRAINT "mLATESTNEWS_pkey";
       public            postgres    false    257            k   �  x��X�n�F]K_qWED�zxc(�����rȆ�h��L
�4@6m�Y]n���c��� �6ݴ��O���E�zPql|�=s�̹w����o;-��g:CsǷ]v]6�mTB�%Z{{����(���RTg�DkKD�Cض,v����	}>��}ӳw9F�D�S�fk�����5nu��Yw:��-�X_k������� n�nol��ޝ����tk��m�Z�.lm�֝�nlv�i�o�
�k��x�&�+�U����m�])��j ��Z�yh���{G�����c:;��>��-xd���[�cb�ác�v���[��`�� JP�'�홏�8��č'���q豈����2%e�H�RR��P��Dip�|��@�(�*I�9]��(�%l��4����%����젧���Ō��P��/Q!-g��;���J&󎂲 ��ZW��" ���T�R�FV��2����ᔬhfKc�����Қ�]k�}Ī́��б�<�S�r�m��r|�{���3� 4�ECС�����&SLARO�������BVd�ϼ�sj>�LiYL]4�.i���r�z��DP]���*)�tVR��8x�X�l�,�~&߼	�lՈ̡ɵrM�.�tVI'Y��+�]ͼ�ɍ@ϻ�%M�
%I��N�Z�ei���-K���
��ￖ�(�r%ծ�Lũ=������&��U���%��)�q��+WbNY~�әW[l��F�h5on۫�fl<k8�v�!v/Y�X'ûc��r���=����b�=���B������V,n�}s�z|�}���t̾"ʙV˨9U���rbds$��7&m>�T��I�������	Y�+��B�@j�B�ӛ��$c�XV8ĵ�� �+э�Z��W���5*(�S_P�&2В�N/FT/���I	��ZN������Z��Zz�Ϸ��~��st5D���"�W����v )�sj���dz�j|��/����ٹ`!V��6��b�~��q����~�M[���'�/�ʉ׈x�Z���[ٹ����wup ��ܢ�%U���1��+�M� ��h_�ʬ4�W��Τ���x��,SFK�-F!~�7a�_��p<
�/��y8~���);}�0x���Sv��O��8�!'�џ�&~�Z�OD�
v�R�t���0�d7�b�+�m�z�04�N#q�l���w$�v��ί<����G�Ire!�r(_��)HԷQ���g�ۃ����j�y���MD�;��B�Q�u�����}���Ob����Jp:Fd1�?�4�ⓏEL�𽠝
������h�� b2�G�rv
�y����p;�Jx��6�]0�E�P�^Į�f�;�|�Y4��	�@�ElH�X��k�y���M�D�}����;yS     