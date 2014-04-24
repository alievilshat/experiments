<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select distinct m.id, c.businessid, c.buyprice, c.salesprice, c.buypricepercent, c.salespricepercent, pbc.countryid from str_paymentwebtype m inner join str_paymentmethodecost c on c.paymentmethodid = m.id inner join str_paymentwebtypebycountry pbc on pbc.paymentgatewayid = c.paymentgatewayid and pbc.businessid = c.businessid')">
          <productbusiness>
            <productbusinessid m:left="-112" m:top="101">pk:productbusiness(payment-<xsl:value-of select="id" />-<xsl:value-of select="countryid" />-<xsl:value-of select="businessid" />)</productbusinessid>
            <productid m:left="-222" m:top="179">fk:product(payment-<xsl:value-of select="id" />-<xsl:value-of select="countryid" />-<xsl:value-of select="businessid" />)</productid>
            <buyprice m:left="-82" m:top="259">
              <xsl:value-of select="buyprice" />
            </buyprice>
            <salesprice m:left="52" m:top="204">
              <xsl:value-of select="salesprice" />
            </salesprice>
            <paymentgatewaypercentageofordersum m:left="-65" m:top="363">
              <xsl:value-of select="buypricepercent" />
            </paymentgatewaypercentageofordersum>
            <salespricepercent m:left="-75" m:top="513">
              <xsl:value-of select="salespricepercent" />
            </salespricepercent>
            <businessid m:left="-78" m:top="439">
              <xsl:value-of select="businessid" />
            </businessid>
            <iswebsite m:left="-61" m:top="311">1</iswebsite>
          </productbusiness>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>