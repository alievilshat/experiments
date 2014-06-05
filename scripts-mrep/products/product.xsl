<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select p.*, ws.title, ws.metakeywords, ws.metadescription, ps.obsolete as subsidiaryobsolete from str_product p left join cms_websiteitems ws on ws.productid = p.id inner join str_brand b on b.id = p.brandid inner join str_category c on c.id = p.categoryid inner join str_productsubsidiary ps on ps.productid = p.id where not p.deleted and (p.parentid is null or p.parentid in (select id from str_product where not deleted))))')">
          <product>
            <productid m:left="-154" m:top="50">
              <xsl:value-of select="id" />
            </productid>
            <parentid m:left="44" m:top="105">
              <xsl:value-of select="user:product(parentid)" />
            </parentid>
            <name m:left="-256" m:top="405">
              <xsl:value-of select="name" />
            </name>
            <description m:left="-126" m:top="440">
              <xsl:value-of select="description" />
            </description>
            <manufacturercode m:left="-154" m:top="100">
              <xsl:value-of select="manufacturercode" />
            </manufacturercode>
            <categoryid m:left="-156" m:top="132">
              <xsl:value-of select="categoryid" />
            </categoryid>
            <brandid m:left="46" m:top="140">
              <xsl:value-of select="brandid" />
            </brandid>
            <producttypeid m:left="-154" m:top="168">
              <xsl:value-of select="producttype" />
            </producttypeid>
            <externalkey m:left="48" m:top="205">
              <xsl:value-of select="externalkey" />
            </externalkey>
            <measurementid m:left="-153" m:top="204">
              <xsl:value-of select="measurementid" />
            </measurementid>
            <standalone m:left="-177" m:top="357">
              <xsl:value-of select="standalone" />
            </standalone>
            <quantity m:left="-176" m:top="273">
              <xsl:value-of select="quantity" />
            </quantity>
            <sequence m:left="-38" m:top="294">
              <xsl:value-of select="sequence" />
            </sequence>
            <isbatch m:left="104" m:top="321">
              <xsl:value-of select="isbatch" />
            </isbatch>
            <onechoice m:left="-174" m:top="318">
              <xsl:value-of select="onechoice" />
            </onechoice>
            <netweight m:left="-38" m:top="336">
              <xsl:value-of select="lastdaysrange" />
            </netweight>
            <minimumquantity m:left="-290" m:top="252">
              <xsl:value-of select="minimumquantity" />
            </minimumquantity>
            <barcode m:left="-153" m:top="239">
              <xsl:value-of select="barcode" />
            </barcode>
            <weight m:left="-323" m:top="210">
              <xsl:value-of select="weight" />
            </weight>
            <taxcategoryid m:left="-111" m:top="392">1</taxcategoryid>
            <metatitle m:left="-256" m:top="540">
              <xsl:value-of select="title" />
            </metatitle>
            <metadescription m:left="-251" m:top="457">
              <xsl:value-of select="metadescription" />
            </metadescription>
            <metakeywords m:left="-126" m:top="520">
              <xsl:value-of select="metakeywords" />
            </metakeywords>
          </product>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
  <msxsl:script implements-prefix="user" language="CSharp"><![CDATA[
    public string product(string key)
    {  if (string.IsNullOrEmpty(key)) return "NULL";
        return "fk:product(" + key + ")";
    }
]]></msxsl:script>
</xsl:stylesheet>