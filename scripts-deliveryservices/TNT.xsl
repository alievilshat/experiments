<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/bodyview3">
    <xsl:for-each select="query">
      <root>
        <record>
          <Ref_Nr_Zending m:left="-4" m:top="121">
            <xsl:value-of select="deliveryid" />
          </Ref_Nr_Zending>
          <Ref_Nr_Afzender m:left="-127" m:top="137">
            <xsl:value-of select="businessname" />
          </Ref_Nr_Afzender>
          <Ref_Nr_Klant m:left="-5" m:top="154">
            <xsl:value-of select="clientnr" />
          </Ref_Nr_Klant>
          <Naam_contract m:left="-130" m:top="170">
            <xsl:value-of select="servicename" />
          </Naam_contract>
          <ProductCode m:left="-129" m:top="201">
            <xsl:value-of select="innerdeliverytype" />
          </ProductCode>
          <Remboursbedrag m:left="-129" m:top="232">
            <xsl:value-of select="totalprice" />
          </Remboursbedrag>
          <Rembours_kenmerk m:left="-6" m:top="248">
            <xsl:value-of select="orderslist" />
          </Rembours_kenmerk>
          <Afzender_account_IBAN m:left="-129" m:top="264">
            <xsl:value-of select="deliveryserviceiban" />
          </Afzender_account_IBAN>
          <Verzekerd_bedrag m:left="-6" m:top="281">
            <xsl:value-of select="insuredamount" />
          </Verzekerd_bedrag>
          <Inhoud m:left="-3" m:top="356">
            <xsl:value-of select="numberofproducts" />
          </Inhoud>
          <Gewicht_zending m:left="-3" m:top="386">
            <xsl:value-of select="fullweight" />
          </Gewicht_zending>
          <Aantal_zendingen m:left="-126" m:top="404">1</Aantal_zendingen>
          <Aantal_colli m:left="-1" m:top="421">
            <xsl:value-of select="deliveryquantity" />
          </Aantal_colli>
          <Opmerking_zending m:left="-125" m:top="438">
            <xsl:value-of select="shipmentremark" />
          </Opmerking_zending>
          <Naam_1_geaddresseerde m:left="-126" m:top="467">
            <xsl:value-of select="clientname" />
          </Naam_1_geaddresseerde>
          <Naam_2_geadresseerde m:left="-4" m:top="484">
            <xsl:value-of select="companyname" />
          </Naam_2_geadresseerde>
          <Straat_geadresseerde m:left="-4" m:top="515">
            <xsl:value-of select="deliveryaddress" />
          </Straat_geadresseerde>
          <Huisnummmer_geadresseerde m:left="-128" m:top="531">
            <xsl:value-of select="deliveryhousenumber" />
          </Huisnummmer_geadresseerde>
          <Huisnummer_toevoeging_van_geaddresseerde m:left="-4" m:top="549">
            <xsl:value-of select="deliveryhousenumberadd" />
          </Huisnummer_toevoeging_van_geaddresseerde>
          <Postcode_geadresseerde m:left="-129" m:top="567">
            <xsl:value-of select="deliveryzipcode" />
          </Postcode_geadresseerde>
          <Plaats_geadresseerde m:left="-4" m:top="584">
            <xsl:value-of select="deliverycity" />
          </Plaats_geadresseerde>
          <Land_geadresseerde m:left="-126" m:top="601">
            <xsl:value-of select="countryname" />
          </Land_geadresseerde>
          <Telefoon_geadresseerde m:left="-4" m:top="617">
            <xsl:value-of select="phonebusiness" />
          </Telefoon_geadresseerde>
          <Mobiele_telefoon_geadresseerde m:left="-126" m:top="633">
            <xsl:value-of select="phonemobile" />
          </Mobiele_telefoon_geadresseerde>
          <Fax_geadresseerde m:left="-4" m:top="649">
            <xsl:value-of select="fax" />
          </Fax_geadresseerde>
          <Email_geadresseerde m:left="-127" m:top="665">
            <xsl:value-of select="email" />
          </Email_geadresseerde>
        </record>
      </root>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>