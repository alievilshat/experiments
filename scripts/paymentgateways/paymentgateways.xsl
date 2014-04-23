<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('str_paymentsettings')">
          <paymentgatewaybusiness>
            <paymentgatewaybusinessid m:left="-227" m:top="101">pk:paymentgatewaybusiness(<xsl:value-of select="paymentgatewaytypeid" />-<xsl:value-of select="businessid" />)</paymentgatewaybusinessid>
            <paymentgatewayid m:left="6" m:top="177">fk:company(payment-<xsl:value-of select="paymentgatewaytypeid" />)</paymentgatewayid>
            <businessid m:left="-129" m:top="194">
              <xsl:value-of select="businessid" />
            </businessid>
            <url1 m:left="1" m:top="212">
              <xsl:value-of select="url" />
            </url1>
            <bankaccountid m:left="20" m:top="320">
              <xsl:value-of select="bankaccountid" />
            </bankaccountid>
            <siteid m:left="28" m:top="386">
              <xsl:value-of select="siteid" />
            </siteid>
            <securecode m:left="3" m:top="241">
              <xsl:value-of select="sitesecurecode" />
            </securecode>
            <ipaddress m:left="-122" m:top="260">
              <xsl:value-of select="defaultipaddress" />
            </ipaddress>
            <successreturnurl m:left="19" m:top="291">
              <xsl:value-of select="successreturnurl" />
            </successreturnurl>
            <siteaccount m:left="-96" m:top="401">
              <xsl:value-of select="account" />
            </siteaccount>
            <reference1 m:left="-104" m:top="335">
              <xsl:value-of select="reference1" />
            </reference1>
            <reference2 m:left="21" m:top="351">
              <xsl:value-of select="reference2" />
            </reference2>
            <reference3 m:left="-106" m:top="368">
              <xsl:value-of select="reference3" />
            </reference3>
            <url2 m:left="-123" m:top="226">
              <xsl:value-of select="url2" />
            </url2>
          </paymentgatewaybusiness>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>