PGDMP         .            	    {            stock    15.3    15.2     {           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            |           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            }           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            ~           1262    41917    stock    DATABASE     x   CREATE DATABASE stock WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_India.1252';
    DROP DATABASE stock;
                postgres    false            �            1259    42016    mCropVariety    TABLE     �   CREATE TABLE public."mCropVariety" (
    "Variety_Code" character varying(5) NOT NULL,
    "Crop_Code" character varying(4) NOT NULL,
    "Variety_Name" character varying(100),
    "IS_ACTIVE" bigint DEFAULT 1,
    "STATUS" bigint DEFAULT 1
);
 "   DROP TABLE public."mCropVariety";
       public         heap    postgres    false            x          0    42016    mCropVariety 
   TABLE DATA           l   COPY public."mCropVariety" ("Variety_Code", "Crop_Code", "Variety_Name", "IS_ACTIVE", "STATUS") FROM stdin;
    public          postgres    false    235   A       �           2606    42088    mCropVariety mCropVariety_pkey 
   CONSTRAINT     y   ALTER TABLE ONLY public."mCropVariety"
    ADD CONSTRAINT "mCropVariety_pkey" PRIMARY KEY ("Variety_Code", "Crop_Code");
 L   ALTER TABLE ONLY public."mCropVariety" DROP CONSTRAINT "mCropVariety_pkey";
       public            postgres    false    235    235            x      x��[Yw�Ǝ~v~�s.�.�X��Z(���yab�Xݶ;G�;���U���̝���kA����Jțe�i����u�����ES�o"u{#n�Og�'�'���8GRQ )Io��� _���Je�����;��wB���w��׻�t_�'b���!��Ss��a����8���f����mPܜl�G���%́C���P�i�[(�;��Y�m�&H4���M7�Z{F�4���W����ˢ_n�X$�P���Hǐ7�C[�B�_�C#��tAt�[�~^�B�2Г�dW}�ho#�F�z��"5R��$�I2��#]D��L*d�L*0-ۨ<��hU�&�~�H䙐̨���D:�vb���d�y��s��҈bb�[}8���zu�P��`�nhkbʑ��UF�"жu�nV���'��hn��]U�uW���D��o�n�X�A"i�����ܣ��$ַa����mDܚ�v	g]ո�RZL�@Z��A"��]0O�<[�k�W	���Aat��{<v�vƥ�)���6�������"��ޞ��6��fg��ݶ�p�DƬ�Y�sY�AV�1fŐR}`�|�kdbx�%�'�DM��V�D��7�#ҔNH���E�،?���喆��B�&�f��t�jA��8��k���8]����J㵜���h�0>G����8���Q9�<��x���qT�
�������>�FJ{د��%uW)uf��*l�ٝ[x�D�Dn]H�@�z���_Fj;~���{^ޮ�b@�������$6����y�Ӣ��o��%޾��瑆
4�뒏d��>���%������즼ܹ��˯�Qy?>�r�k˘��ʜ,��)�f��G�cY[>P�����X�KA�|��)R�@=��bE������f�����v�A�r| ��)������Z�<�m���4�O&����D$�o�<���F,�o���U���A�s-��P�'�Q7���Wڽ�dR'��D̓���H&�Q:�O��3�㧶�}�gbӊ�&8�-l��;h�m6vKܢ���K��4�ԀMd.¨�OpcC�rOz7DFĒY$B	���򅗱��4l�=p�v+���Q&��r�����x��x��xƻjG�:g��C$	3x_��-�L����>.r!�uٮ��M���mg]�/.��7E:�;�=o�~h�<���:�5w:$+�qUխ�3���% �Z�$�ө���l6q���<����;`�; �!<�����x�ƶ���>sW۬���ݚd��$"��o���L���g������x�h_F,.�_�靊��j�x�ڍ�� ץ5}��X�7<��_O0l�yPB^����݊d:%
	UՁe`��ʲ��V�ʜ�P.P�T+�4��@������9l�����ݕ]���W{0
�!*��ޞ֝wF��h� �{���,@,ܩ�� ׷����/ԃ0���'aN�s|������`7����KS�H�rw]!$7^��l�?|#��@n�@NV�B��6`�cmCH��G�8 p�{ S����95	�� �c���t��Mc��$*�Įvh��o���#/xj!��� ���v�;k!�JɦW��`��`D�����ڙߨJ��ʺ_)8��S��)��<_�mDB���<������F���9-ڦu �[�C��B��w��FȔ�!.���kwD�0�r�ŭ�$O�Ed�!��ׁj��'pX����i���D���\9�e��w��4H+t�gx=]������|� $���Wj�O�J���0n���W �`>xˍ=�׍{]1��
�:̎O�Y��NK9�H�Q̄��Q�G�N1c�1�O$*��xl��@���� �Y��t��vӛe��-ע�u���cH�2��I��5�7�L��<d~R���K��b<��l�9��D1lfd�Ȃ�~FD53� �JfTID1#�D�3bAD5#"ꉨa��mlXj������R2�LT6cP�E^����&"zk?;��@�H�
�e����u�&v���9;�l�޾c'6[��:�*�G����c�1�`��dMȮ9�ih�S����DBqݔϠ*SE��7:�ry�����2A蚼W�̈�����T��'3�<n)?Q����CI6UW��o�H���)�$��dR���߇bK�w�l $N	 �?�)O���qV��Q`��
̊'4�ZS�{�$���t �o�c$R�:� Adl7(V���  ��ע�5=cʘ��G�8���Yrf�Ǚ�'!��7��'�s�G��N��ܸ�j�=��N@��	�2gu�P����������96=����eQ�s�h�(�9���@�.9BĔ�T[����I ����Y��7[�l:7�K�ı_ը^jq���,�)Jf�O�Uʶ.3����)KHXs��G L
�[�
����Ik��v��v�̆b޹��m��H��ARо����%�W�)�d������+��"�����9��vR+��%���sł�s���0�Y �`&��~h��Ah$�	πI��0���?��F���z7E�	e&u�{�8A���D��+{�3�f� �<|�[�S�@lQ�ݲ�{C�V<)(Z��!1�.��S������h1�@�.�C����'`n��f��6�O|��d���ȏlmߨ�P�2�d��AC[o)���	;��@nƢEl�2�f���t<�Y_�i�����f6籵?s�"A�@(	7��1@z��"�X�c��8��_�)���~*t ���\�-��^��Ap���	ݔ�Wf������:��ɝ�u&����[��#�0/c�vxd�-�Q�9�2�����u'	Qc(I�M��G���2��
C;�� ȟF�@^o�f\��y/*����k�gBOS�y�L3�$[UA���{�|i��=`-����v�S����vC�E�ob68Һ%�k�[  (9�B8�L�\�$�V3��)s�q>�v`$��D�7T�P`7�?%ͯD��3�������x�t"��NL5�ZRc��2���3�*��Tv0m@����,��E6�*=����:��1�L�����򨗛3��`��򝠘�ޑ��	���)a��wdCd�ǩ��.VU<�	���!Szt�)��Q
]���6h<6x��&҄���ө��9���������3�30�떵6%%8m �#"�7��&���)�Nz>{����YA8^��fv�=	^�ԃ��XQ$*���ɚo�
0e�v6#��Tm����zX��w�L�S�J��i�3���H�3L�S���xܐ;oC~ߕx:����tj{ 8[�����?�����z�E�S�S��^Bv7M8�.�@��dB��/RxVg���gk������z��ǆ[2�T� V5vFɘ��?�åq�˺y^�E��v��O#s(&<��e�e�U`�UbW��R�
/`v�R`��>"p)t`����� *Vx�"'�a}w�v��i�ϟ����������P�tF��8��)S~E�|��4��	NC�����h��H`�b=�:�B	�;�\��jܔ*��Le���n���~�S�|�"c�༧rL ���f
(��r5�����@O��u��U�X-)�=n �`g��xER���eh`gS��7��28iԻ����-a��O[�YD�4�����wεc���Y��wol,��R��:��wH�f.��>��S�#���:5E�[��������f��hy<��]ݯ�	�ԇO�� ���=o}]M���r	R�M���"���	���v�a���7Ƞܠo9�d �fh-L���3�q��c��4^���t�`�}��;1�1�l�8*3|^;0uL��ut"�0x7>�w��G^��Z����Ѣ�Ց0���[f23&�����ip����2��mJY��Vx�?���ߺf����tEz��Kj;_��/�����1(���<�8K$f�������P.n\�^��M[[;�F�mut��N<0CP����=㐯���~#�<>�!`�u�ζ��wЦ�� �  ����`)]>0�Б�%�4�5�6��(���ɵ.�]��?�d-����}�c��R/Lz+��>>o}�>����e̥v�~`��=X�j]η��	����ns���?��e��g�X� �3{���� �:t/ሏ�ݣ�;���7����3	ỹ)m�sT�mˍ'Ҩ���ƥ����~k.�K_żu��sy�c�R�'&���W�9�4�$]��`@D�+��w-�: R�਄�E�Ϯ�x��F\w�ۗ�:�F��ѧ�hx�m��$�|{��J��l�4�q�ȉ��.�F:Sp	���ю�+�("�_����DU7����6����@���*�Km�=�^Ǩ�<��6Fge�g��<@�"�_���*`]��������噶B���tF�W�ux~�J���\t��������<2�
j5��/?��Y���W8�`���[;��d$>��a�\�_��Dˇ�#�#�l�c6�JMV>F#����)�P���?�b��Y\E��)�R`�"���5�C�&Yj���ֶ=�w�)�)��K�c|��=ݻN���^�k<�����;pB4���hF��_�4��bq����:��j���>�\hc0��_��

�#���������4ui��n7{X6_��������4KK�v���6+�����QBž=�������y��Խ�<\��r�u�4D·v ��p���w1C0_�������3T��r;���[�n��잞&U��P��H�"�Ƀa&�vx�ƿmn�N(\0^�ԛ�DU3*5F���q�0�\M�PM�aC�됁���54�8�df�@Ly����ʜ�:$�o߸+�(|��Z��|�\ꆻ�M��op1<?��$@"�\������?D^���ҍ�����D}/$5 ���Q�>n �K�IҶ��y>�c	7�Psߡ��  �,�Cfh�ڗ҅�)��%G�Ƈ"�610�d�6��\Ƣ%Z�0�O�d�D�]��$����5���>�&+hK?x�IΡ��I$����Di��1dΩ0e|� �ܴ��A?:����"4S�4s�D��۞��)0��ӖI21�� m��,�K3w�� ���ˊP�)��mkJܙ#����'�f�kS��c7�%�@C�	�<<+��U1������9%�f��2D��1lC K �X�v(�~rTN�l����J�S�'Ō��؄>G�{;!8c�/��-E &b�
�u�S�3�K�T�p�-O�Ytrٞ�!� |�l��_L�6���C�rO�x�0�+UGa�LS���t<n��Y�~!���ϡD���[J�1�h���F��Z|v�Krq� �RE�wQ�\w$|&|�(�4w���N� j�J���%D"W�"��b}���5�%�������m�%˲�/�[j�)8��^;@$.VUO�e9'�=����7k�: ��sp`@?O06�&q*�����<>NP��݆�`~��v��@jв����{�I# ���w|j7�wP4L�ڹ���#�����:+C�"9��� V (�������V"lZ���=��?������bj�q�ƨ{���=�j|�B -��gI�M�L@�Hbt����������r=2x/���Z��W!۫���*���c'���~��M��K�/�;@LX́�釃i�(37���
����|rz5�D���=VS'��݅_����� W�ߍw����__.aO��o?����k>�/     