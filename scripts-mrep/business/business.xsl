<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:table('sys_business')">
          <address>
            <addressid m:left="190" m:top="2" xmlns:m="http://www.navitas.nl/2014/Mapper">pk:address(<xsl:value-of select="id" />)</addressid>
            <streetaddress m:left="190" m:top="76" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="address" />
              <xsl:value-of select="addressadd" />
            </streetaddress>
            <zipcode m:left="190" m:top="188" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="zipcode" />
            </zipcode>
            <city m:left="194" m:top="224" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="city" />
            </city>
            <countryid m:left="196" m:top="260" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="countryid" />
            </countryid>
            <housenumber m:left="190" m:top="116" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="housenumber" />
            </housenumber>
            <housenumberadd m:left="192" m:top="150" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="housenumberadd" />
            </housenumberadd>
            <recipient m:left="190" m:top="42" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="name" />
            </recipient>
          </address>
          <company>
            <companyid m:left="-26" m:top="164" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="id" />
            </companyid>
            <phone m:left="-28" m:top="212" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="phone" />
            </phone>
            <email m:left="-28" m:top="284" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="email" />
            </email>
            <fax m:left="-28" m:top="246" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="fax" />
            </fax>
            <url m:left="-32" m:top="376" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="url" />
            </url>
            <companyname m:left="-28" m:top="456" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="name" />
            </companyname>
            <taxnumber m:left="-28" m:top="316" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="vatnumber" />
            </taxnumber>
            <chamberofcommerce m:left="-30" m:top="346" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="chambercommerce" />
            </chamberofcommerce>
            <addressid m:left="-28" m:top="412" xmlns:m="http://www.navitas.nl/2014/Mapper">fk:address(<xsl:value-of select="id" />)</addressid>
            <companytypeid m:left="-24" m:top="492" xmlns:m="http://www.navitas.nl/2014/Mapper">1</companytypeid>
            <iban m:left="-34" m:top="584" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="ibannumber" />
            </iban>
            <urlpreview m:left="-32" m:top="664" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="url" />
            </urlpreview>
            <languageid m:left="-34" m:top="704" xmlns:m="http://www.navitas.nl/2014/Mapper">1</languageid>
            <urlmobile m:left="-32" m:top="692" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="url" />
            </urlmobile>
            <urlpreviewmobile m:left="-32" m:top="720" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="url" />
            </urlpreviewmobile>
            <bankaccount m:left="-34" m:top="540" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="account" />
            </bankaccount>
          </company>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>