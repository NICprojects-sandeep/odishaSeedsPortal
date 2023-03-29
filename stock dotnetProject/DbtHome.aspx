<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DbtHome.aspx.cs" Inherits="DbtHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DBT</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link rel="stylesheet" href="layout/styles/layout.css" type="text/css" />
    <script type="text/javascript" src="layout/scripts/jquery.min.js"></script>
    <script type="text/javascript" src="layout/scripts/jquery.jcarousel.pack.js"></script>
    <script type="text/javascript" src="layout/scripts/jquery.jcarousel.setup.js"></script>
</head>
<body id="top">
    <!-- ####################################################################################################### -->
    <div class="wrapper col1">
        <div id="header">
            <div id="logo">
                <img src="images/DBT.png" alt="" />
            </div>
            <div id="topnav">
                <ul>
                    <li class="active"><a href="http://agrisnetodisha.ori.nic.in">Agrisnet Home</a></li>
                    <li><a href="Login.aspx">LogIn</a>
                        <%--<ul>
            <li><a href="StockLoginOAIC.aspx">For OAIC</a></li>
            <li><a href="StockLoginOSSC.aspx">For OSSC</a></li>
          </ul>--%>
                    </li>
                    <li><a href="#">Report</a>
                        <ul>
                            <li><a href="Reports/Stock_cropwise_staterpt.aspx">State Report Cropwise</a></li>
                            <li><a href="Reports/Stock_DistRpt.aspx">DistrictWise Stock Details</a></li>
                            <li><a href="Reports/Stock_GoDownRpt.aspx">GodownWise Stock Details</a></li>
                            <li><a href="Stock_StateWise_Report.aspx">StateWise Variety Stock Details</a></li>
                            <li><a href="Stock_State_Godown_Report.aspx">StateWise Godown Stock Details</a></li>
                            <li><a href="Stock_Statewise_TotalDetails_Report.aspx">StateWise Stock Details</a></li>
                            <%--<li><a href="Stock/Stock_Crystal_CropWise.aspx">Daily Report</a></li>--%>
                            <li><a href="Stock_Crystal_Month_CropWise.aspx">MonthWise Daily Report</a></li>
                            <li><a href="Stock_Satewise_total_datewise.aspx">Daily Report Datewise</a></li>
                        </ul>
                    </li>
                    <li><a href="Log_In.aspx?p=0">Reports For DDA/OSSC/OAIC</a></li>
                    <%--<li><img src="images/Home.png" alt=""></img></li>--%>
                    <%--<li class="last"><a href="Log_In.aspx?p=1">Admin Login</a></li>--%>
                </ul>

            </div>
            <br class="clear" />
        </div>
    </div>
    <!-- ####################################################################################################### -->
    <div class="wrapper col2">
        <div id="featured_slide">
            <div id="featured_content">
                <ul>
                    <%--<li>
                        <img src="layout/images/new-seeds.jpg" alt="" />
                        <div class="floater">
                            <h2>
                                About SEED DBT</h2>
                            <p>
                                This Programme envisages a switch from the present electronic transfer of benefits to bank accounts of the beneficiary to transfer of benefits directly to BKKY seeded bank accounts of the beneficiaries. The other objectives are:
•	Accurate Targeting 
•	De-duplication
•	Reduction of Fraud 
•	Process Re-engineering of Schemes for simpler flow of information and funds.
•	Greater Accountability

                            </p>                            
                        </div>
                    </li>--%>
                    <li>
                        <img src="images/demo/Seed_germination.png" alt="" />
                        <div class="floater">
                            <h2>Seeds Input Management  </h2>
                            <p>
                                Seed is one of the most important inputs that play a key role in boosting agricultural
                                productivity. Keeping other inputs of production constant, the quality seeds alone
                                can increase the production to the extent of nearly 20%.
                            </p>
                            <%--<p class="readmore">
                                <a href="#">Continue Reading &raquo;</a></p>--%>
                        </div>
                    </li>
                    <li>
                        <img src="images/demo/6558_1280x800.jpg" alt="" />
                        <div class="floater">
                            <h2>Policies on Seed</h2>
                            <p>
                                Appropriate policy framework and programmatic interventions will be adopted to stimulate
                                varietal development in tune with market trends, scientific-technological advances,
                                suitability for biotic and abiotic stresses, locational adaptability and farmers'
                                needs.
                            </p>
                            <%--<p class="readmore">
                                <a href="#">Continue Reading &raquo;</a></p>--%>
                        </div>
                    </li>
                    <li>
                        <img src="images/demo/9985961_orig.jpg" alt="" />
                        <div class="floater">
                            <h2>Seeds Input Management</h2>
                            <p>
                                Seed is one of the most important inputs that play a key role in boosting agricultural
                                productivity. Keeping other inputs of production constant, the quality seeds alone
                                can increase the production to the extent of nearly 20%.
                            </p>
                            <%--<p class="readmore">
                                <a href="#">Continue Reading &raquo;</a></p>--%>
                        </div>
                    </li>
                    <li>
                        <img src="images/demo/brote_semilla_dinafem_photo_gallery_full.jpg" alt="" />
                        <div class="floater">
                            <h2>Policies on Seed</h2>
                            <p>
                                Appropriate policy framework and programmatic interventions will be adopted to stimulate
                                varietal development in tune with market trends, scientific-technological advances,
                                suitability for biotic and abiotic stresses, locational adaptability and farmers'
                                needs.
                            </p>
                            <%--<p class="readmore">
                                <a href="#">Continue Reading &raquo;</a></p>--%>
                        </div>
                    </li>

                    <li>
                        <img src="images/demo/cuándo-plantar-marihuana-en-exterior.jpg" alt="" />
                        <div class="floater">
                            <h2>Policies on Seed</h2>
                            <p>
                                Appropriate policy framework and programmatic interventions will be adopted to stimulate
                                varietal development in tune with market trends, scientific-technological advances,
                                suitability for biotic and abiotic stresses, locational adaptability and farmers'
                                needs.
                            </p>
                            <%--<p class="readmore">
                                <a href="#">Continue Reading &raquo;</a></p>--%>
                        </div>
                    </li>

                </ul>
            </div>
            <a href="javascript:void(0);" id="featured-item-prev">
                <img src="layout/images/prev.png" alt="" /></a> <a href="javascript:void(0);" id="featured-item-next">
                    <img src="layout/images/next.png" alt="" /></a>
        </div>
    </div>
    <!-- ####################################################################################################### -->
    <div class="wrapper col3">
        <div id="container">
            <div class="homepage">
                <ul>
                    <li>
                        <h2>
                            <img src="images/demo/downloadseed.jpg" alt="" style="width: 60px; height: 60px;" />Certification
                            Agency</h2>
                        <p>
                            Breeder seed is seed or vegetative propagating material directly controlled by the
                            originating or sponsoring plant breeder of the breeding programme or institution
                            and/or seed whose production is personally supervised by a qualified plant breeder
                            and which provides the source for the initial and recurring increase of Foundation
                            seed.
                        </p>
                        <%-- <p class="readmore">
                            <a href="#">Continue Reading &raquo;</a></p>--%>
                    </li>
                    <li>
                        <h2>
                            <img src="images/demo/download.jpg" alt="" style="width: 60px; height: 60px;" />Classes
                            and Sources of Seed</h2>
                        <p>
                            The purpose of seed certification is to maintain and make available to the public,
                            through certification, high quality seeds and propagating materials of notified
                            kind and varieties so grown and distributed as to ensure genetic identity and genetic
                            purity. Seed certification is also designed to achieve prescribed standards.
                        </p>
                        <%--<p class="readmore">
                            <a href="#">Continue Reading &raquo;</a></p>--%>
                    </li>
                    <li class="last">
                        <h2>
                            <img src="images/demo/images.jpg" alt="" style="width: 60px; height: 60px;" />Export/Import
                            Policy On Seed and Planting material</h2>
                        <p>
                            import of seeds of wheat, rye, barley, oat, maize, rice, millet, jowar, bajra, ragi,
                            other cereals, soybean, groundnut, linseed, palmnut, cotton, castor, sesamum, mustard,
                            safflower, clover, jojoba, etc. is allowed without licence subject to the New Policy
                            on Seed Development, 1988 and in accordance with import permit granted under
                        </p>
                        <%--                        <p class="readmore">
                            <a href="#">Continue Reading &raquo;</a></p>--%>
                    </li>
                </ul>
                <br class="clear" />
            </div>
        </div>
    </div>
    <!-- ####################################################################################################### -->
    <div class="wrapper col4">
        <div id="footer">
            <div>
                <p>Designed By National Informatics Centre,Bhubaneswar,Odisha State Centre.</p>
            </div>

            <br class="clear" />
        </div>
    </div>
    <!-- ####################################################################################################### -->
    <div class="wrapper col5">
    </div>
</body>
</html>
