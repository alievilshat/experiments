<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('with product as (select p.id from str_product p inner join str_category c on c.id = p.categoryid where not p.deleted and (p.parentid is null or p.parentid in (select id from str_product where not deleted))) select * from str_productrelated pr inner join product p1 on p1.id = pr.productid inner join product p2 on p2.id = pr.relatedproductid')">
          <productrelated>
            <productrelatedid m:left="-154" m:top="228">pk:productrelated(<xsl:value-of select="productid" />-<xsl:value-of select="relatedproductid" />-<xsl:value-of select="subsidiaryid" />)</productrelatedid>
            <productid m:left="-35" m:top="163">
              <xsl:value-of select="productid" />
            </productid>
            <relatedproductid m:left="-162" m:top="179">
              <xsl:value-of select="relatedproductid" />
            </relatedproductid>
            <businessid m:left="-35" m:top="198">
              <xsl:value-of select="subsidiaryid" />
            </businessid>
          </productrelated>
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