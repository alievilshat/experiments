<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select * from str_stockproduct where productid in (select p.id from str_product p inner join str_category c on c.id = p.categoryid where not p.deleted and (p.parentid is null or p.parentid in (select id from str_product where not deleted))) and businessid != 0 and stockid != 0')">
          <stockproduct>
            <stockproductid m:left="-40" m:top="388" xmlns:m="http://www.navitas.nl/2014/Mapper">pk:stockproduct(<xsl:value-of select="productid" />-<xsl:value-of select="stockid" />-<xsl:value-of select="businessid" />)</stockproductid>
            <stockid m:left="-34" m:top="202" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="stockid" />
            </stockid>
            <productid m:left="-35" m:top="169" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="productid" />
            </productid>
            <businessid m:left="-33" m:top="290" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="businessid" />
            </businessid>
            <quantityinstock m:left="-33" m:top="231" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="quantity" />
            </quantityinstock>
            <iron m:left="-32" m:top="260" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="iron" />
            </iron>
            <damaged m:left="-38" m:top="322" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="damaged" />
            </damaged>
          </stockproduct>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>