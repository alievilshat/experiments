<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select * from str_discountgiveaway o where o.productid in (select id from str_product where not deleted and true and (parentid is null or parentid in (select id from str_product where not deleted and true))) ')">
          <discountgiveaway>
            <discountgiveawayid m:left="43" m:top="160">
              <xsl:value-of select="giveawayid" />
            </discountgiveawayid>
            <groupid m:left="-86" m:top="177">
              <xsl:value-of select="groupid" />
            </groupid>
            <productid m:left="46" m:top="196">
              <xsl:value-of select="productid" />
            </productid>
            <isdefault m:left="-84" m:top="216">
              <xsl:value-of select="isdefault" />
            </isdefault>
          </discountgiveaway>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>