<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select distinct m.*, s.paymentgatewaytypeid as paymentgatewayid, c.businessid, pbc.countryid from str_paymentwebtype m inner join str_paymentmethodecost c on c.paymentmethodid = m.id inner join str_paymentwebtypebycountry pbc on pbc.paymentgatewayid = c.paymentgatewayid and pbc.businessid = c.businessid left join str_paymentsettings s on s.paymentgatewaytypeid = c.paymentgatewayid and s.businessid = c.businessid')">
          <product>
            <productid m:left="-186" m:top="111">pk:product(payment-<xsl:value-of select="id" />-<xsl:value-of select="countryid" />-<xsl:value-of select="businessid" />)</productid>
            <name m:left="-86" m:top="530">
              <xsl:value-of select="name" />
            </name>
            <description m:left="36" m:top="547">
              <xsl:value-of select="description" />
            </description>
            <producttypeid m:left="-21" m:top="175">8</producttypeid>
            <sequence m:left="-3" m:top="373">
              <xsl:value-of select="sequence" />
            </sequence>
            <paymentgatewaybusinessid m:left="-114" m:top="666">
              <xsl:value-of select="user:getgatewayid(paymentgatewayid, businessid)" />
            </paymentgatewaybusinessid>
            <servicecountryid m:left="5" m:top="470">
              <xsl:value-of select="countryid" />
            </servicecountryid>
            <largedescription m:left="-118" m:top="589">
              <xsl:value-of select="id" />-<xsl:value-of select="paymentgatewayid" />-<xsl:value-of select="businessid" /></largedescription>
            <taxcategoryid m:left="-36" m:top="430">2</taxcategoryid>
          </product>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
  <msxsl:script language="C#" implements-prefix="user">
   public string getgatewayid(string paymentgatewayid, string businessid) {
      if (string.IsNullOrEmpty(paymentgatewayid))
      return "NULL";
      else
      return "fk:paymentgatewaybusiness(" + paymentgatewayid+ "-" + businessid + ")";
   }
</msxsl:script>
</xsl:stylesheet>