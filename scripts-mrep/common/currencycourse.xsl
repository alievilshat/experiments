<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:table('str_currencycourse')">
          <currencycourse>
            <currencycourseid m:left="-81" m:top="162">
              <xsl:value-of select="id" />
            </currencycourseid>
            <systemcurrencyid m:left="39" m:top="177">
              <xsl:value-of select="systemcurrencyid" />
            </systemcurrencyid>
            <currencyid m:left="-84" m:top="193">
              <xsl:value-of select="currencyid" />
            </currencyid>
            <coursedate m:left="38" m:top="208">
              <xsl:value-of select="coursedate" />
            </coursedate>
            <coursevalue m:left="-84" m:top="222">
              <xsl:value-of select="coursevalue" />
            </coursevalue>
            <datechange m:left="40" m:top="236">
              <xsl:value-of select="datechange" />
            </datechange>
            <employeeid m:left="-83" m:top="252">fk:person(<xsl:value-of select="employeeid" />)</employeeid>
          </currencycourse>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>