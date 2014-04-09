<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select * from str_product where not deleted and producttype = 0 and categoryid != 0 and (parentid is null or parentid in (select id from str_product where not deleted and producttype = 0 and categoryid != 0))')">
          <product>
            <productid m:left="-156" m:top="66" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="id" />
            </productid>
            <parentid m:left="54" m:top="84" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="user:product(parentid)" />
            </parentid>
            <name m:left="76" m:top="424" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="name" />
            </name>
            <description m:left="-126" m:top="440" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="description" />
            </description>
            <manufacturercode m:left="-154" m:top="100" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="manufacturercode" />
            </manufacturercode>
            <categoryid m:left="-156" m:top="132" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="categoryid" />
            </categoryid>
            <brandid m:left="46" m:top="140" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="brandid" />
            </brandid>
            <producttypeid m:left="-154" m:top="168" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="producttype" />
            </producttypeid>
            <externalkey m:left="-156" m:top="218" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="externalkey" />
            </externalkey>
            <measurementid m:left="-344" m:top="180" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="measurementid" />
            </measurementid>
            <obsolete m:left="-128" m:top="504" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="obsolete" />
            </obsolete>
            <standalone m:left="96" m:top="354" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="standalone" />
            </standalone>
            <quantity m:left="-162" m:top="268" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="quantity" />
            </quantity>
            <sequence m:left="-28" m:top="286" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="sequence" />
            </sequence>
            <isbatch m:left="98" m:top="302" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="isbatch" />
            </isbatch>
            <onechoice m:left="-174" m:top="318" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="onechoice" />
            </onechoice>
            <netweight m:left="-38" m:top="336" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="lastdaysrange" />
            </netweight>
            <minimumquantity m:left="-290" m:top="252" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="minimumquantity" />
            </minimumquantity>
            <barcode m:left="-430" m:top="236" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="barcode" />
            </barcode>
            <weight m:left="-510" m:top="198" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="weight" />
            </weight>
            <taxcategoryid>1</taxcategoryid>
          </product>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
  <msxsl:script implements-prefix="user" language="CSharp"><![CDATA[
    public string product(string key)
    {  if (string.IsNullOrEmpty(key)) return "NULL";
        return "fk:product(" + key + ")";
    }
]]></msxsl:script>
</xsl:stylesheet>