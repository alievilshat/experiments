<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('str_currency')">
          <currency>
            <currencyid m:left="6" m:top="161">
              <xsl:value-of select="id" />
            </currencyid>
            <currencyname m:left="-118" m:top="178">
              <xsl:value-of select="currencyname" />
            </currencyname>
            <currencycode m:left="5" m:top="193">
              <xsl:value-of select="currencycode" />
            </currencycode>
            <currencysymbol m:left="-117" m:top="211">
              <xsl:value-of select="currencysymbol" />
            </currencysymbol>
            <isfixed m:left="9" m:top="227">
              <xsl:value-of select="isfixed" />
            </isfixed>
            <isdefault m:left="-115" m:top="242">
              <xsl:value-of select="isdefault" />
            </isdefault>
            <symbolbehindamount m:left="7" m:top="257">
              <xsl:value-of select="symbolafternumeral" />
            </symbolbehindamount>
          </currency>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>