<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select * from str_stockproduct where businessid = 3 and  productid in (select id from str_product where not deleted and producttype = 0 and (parentid is null or parentid in (select id from str_product where not deleted and producttype = 0))) and productid != 705 and productid &lt; 2598')">
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