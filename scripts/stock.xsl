<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('str_stock')">
          <stock>
            <stockid m:left="53" m:top="249" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="id" />
            </stockid>
            <stockname m:left="60" m:top="288" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="name" />
            </stockname>
            <email m:left="63" m:top="321" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="email" />
            </email>
            <phone m:left="72" m:top="359" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="phone" />
            </phone>
            <addressid m:left="70" m:top="390" xmlns:m="http://www.navitas.nl/2014/Mapper">fk:address(<xsl:value-of select="id" />)</addressid>
          </stock>
          <address>
            <addressid m:left="-82" m:top="55" xmlns:m="http://www.navitas.nl/2014/Mapper">pk:address(<xsl:value-of select="id" />)</addressid>
            <recipient m:left="-80" m:top="121" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="name" />
            </recipient>
            <streetaddress m:left="-79" m:top="153" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="address" />
            </streetaddress>
            <housenumber m:left="-82" m:top="185" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="housenumber" />
            </housenumber>
            <housenumberadd m:left="-84" m:top="216" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="housenumberadd" />
            </housenumberadd>
            <zipcode m:left="-86" m:top="248" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="zipcode" />
            </zipcode>
            <city m:left="-85" m:top="284" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="city" />
            </city>
            <countryid m:left="-84" m:top="318" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="countryid" />
            </countryid>
          </address>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>