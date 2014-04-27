<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select ds.*, dt.type as deliverytype, sb.stockid from str_deliveryservice ds inner join str_deliverytype dt on dt.id = ds.deliverytypeid inner join str_stockbusiness sb on sb.businessid = ds.businessid')">
          <productbusiness>
            <productbusinessid m:left="-110" m:top="161">pk:productbusiness(delivery-<xsl:value-of select="id" />-<xsl:value-of select="stockid" />-1)</productbusinessid>
            <productid m:left="12" m:top="177">fk:product(delivery-<xsl:value-of select="id" />-<xsl:value-of select="stockid" />-1)</productid>
            <salesprice m:left="-111" m:top="194">
              <xsl:value-of select="price" />
            </salesprice>
            <businessid m:left="20" m:top="468">
              <xsl:value-of select="businessid" />
            </businessid>
            <iswebsite m:left="19" m:top="434">1</iswebsite>
          </productbusiness>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>